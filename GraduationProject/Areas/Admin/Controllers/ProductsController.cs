using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Areas.Admin.ViewModels;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository product;
        public ICategoryRepositry category { get; }
        public IBrandRepository brand { get; }
        public IModelRepositry model { get; }
        public IProductAttributesRepository productAttributes { get; }

        public ProductsController(IProductRepository product, ICategoryRepositry category,
            IBrandRepository brand, IModelRepositry model,
            IProductAttributesRepository productAttributes)
        {
            this.product = product;
            this.category = category;
            this.brand = brand;
            this.model = model;
            this.productAttributes = productAttributes;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetProducts(int start = 0, int length = 10)
        {

            string search = HttpContext.Request.Query["search[value]"];

            return Json(product.GetDataTable(start, length, a => a.Name.Contains(search), a => a.Id));
        }
        public ActionResult Create()
        {
            ProductModelBrandViewModel viewModel = new ProductModelBrandViewModel()
            {
                Categories = new SelectList(category.GetAll(), "Id", "Name"),
                Brands = new SelectList(brand.GetAll(), "Id", "Name")
            };
            return PartialView("Create", viewModel);
        }
        [HttpPost]
        public JsonResult GetModelWithCategories(int categoryId)
        {
            return new JsonResult(product.GetModelWithCategories(categoryId));
        }
        [HttpPost]
        public ActionResult GetCategoryAttributes(int categoryId)
        {
            ProductModelBrandViewModel viewModel = new ProductModelBrandViewModel()
            {
                ProductAttributes = product.GetCategoryAttributes(categoryId).ToList()
            };

            return PartialView("ProductAttributes", viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProductModelBrandViewModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product newProduct = new Product()
                    {
                        BrandId = productModel.BrandId,
                        ModelId = productModel.ModelId,
                        Name = productModel.Name
                    };
                    product.Add(newProduct);
                    product.SaveAll();
                    
                    for (int i = 0; i < productModel.ProductAttributes.Count; i++)
                    {
                        ProductAttributes newProductAttributes = new ProductAttributes()
                        {
                            ProductId = newProduct.Id,
                            AttributeName = productModel.ProductAttributes[i].Name,
                            Value = productModel.ProductValues[i]
                        };

                        productAttributes.Add(newProductAttributes);
                    }

                    productAttributes.SaveAll();

                    return Ok("Product has been Add");
                }

                return BadRequest("Product Data is not valid");
            }

            catch
            {
                return BadRequest("Product Data is not valid");
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product deleteProduct = product.Get(id);
                if (deleteProduct == null)
                {
                    return NotFound("The Product is not found");
                }
                var productAttribute = productAttributes.GetProductAttributes(id);
                foreach(var item in productAttribute)
                {
                    productAttributes.Delete(item);
                }
                
                productAttributes.SaveAll();
                product.Delete(deleteProduct);
                product.SaveAll();

                return Ok("The Product has been Deleted");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }
        public ActionResult Edit(int id)
        {
            Product EditProduct = product.Get(id);
            Model GetModel = model.Get(EditProduct.ModelId);
            ViewBag.GetModels = product.GetModelWithCategories(GetModel.CategoryId);
            var attributes = productAttributes.GetProductAttributes(id);
            List<string> GetAttributes = new List<string>();
            foreach(var item in attributes)
            {
                GetAttributes.Add(item.Value);
            }
            ProductModelBrandViewModel viewModel = new ProductModelBrandViewModel()
            {
                CategoryId = GetModel.CategoryId,
                BrandId = EditProduct.BrandId,
                ModelId = EditProduct.ModelId,
                Name = EditProduct.Name,                
                Categories = new SelectList(category.GetAll(), "Id", "Name"),
                Brands = new SelectList(brand.GetAll(), "Id", "Name"),
                ProductAttributes = product.GetCategoryAttributes(GetModel.CategoryId).ToList(),
                ProductValues = GetAttributes,
                ProductId = EditProduct.Id
            };
            return PartialView("Edit", viewModel);
        }
        [HttpPost]
        public ActionResult Edit(ProductModelBrandViewModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product EditProduct = product.Get(productModel.ProductId);
                    EditProduct.ModelId = productModel.ModelId;
                    EditProduct.BrandId = productModel.BrandId;
                    EditProduct.Name = productModel.Name;
                    product.Edit(EditProduct);
                    product.SaveAll();
                    var EditAttributes = productAttributes.GetProductAttributes(productModel.ProductId).ToList();

                    for(int i = 0; i < productModel.ProductAttributes.Count; i++)
                    {
                        EditAttributes[i].Value = productModel.ProductValues[i];
                        productAttributes.Edit(EditAttributes[i]);
                    }

                    productAttributes.SaveAll();

                    return Ok("Product has been changed");
                }

                return Ok("Product Data is not valid");
            }

            catch
            {
                return BadRequest("Product Data is not valid");
            }

        }
    }
}
