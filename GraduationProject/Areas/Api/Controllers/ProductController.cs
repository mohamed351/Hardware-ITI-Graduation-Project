using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.ExtenstionMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly IUserProductImagesRepository _userProductImages;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUserProductRepository userProductRepository
            , IUserProductImagesRepository userProductImages,
            IProductRepository productRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _userProductRepository = userProductRepository;
            _userProductImages = userProductImages;
            _productRepository = productRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddProduct([FromBody]AddProductViewModel model)
        {
            try
            {
                string userId = User.GetUserIdToken();
                if (ModelState.IsValid)
                {
                    UserProduct userProduct = new UserProduct()
                    {
                        Name = model.Name,
                        UserId = userId,
                        ProductId = model.ProductId,
                        Condition = model.Condition,
                        Description = model.Description,
                        Price = model.Price,
                        IsNegotiable = model.IsNegotiable,
                        IsReplacable = model.IsReplacable,
                        DateAdded = DateTime.Now
                 
                    };
                    foreach (var item in model.Images)
                    {
                        userProduct.UserProductImages.Add(new UserProductImages()
                        {
                            Images = SaveAnImages(item)
                        });

                    }
                    _userProductRepository.Add(userProduct);
                    _userProductRepository.SaveAll();
                    return Ok(new { message = "Product Successfully Added." });
                }
                return BadRequest("The Product Information is not valid.");

            }
            catch
            {
                return BadRequest("An Error Has Occured.");
            }
        }


        [HttpGet("GetProduct")]
        [Route("GetProduct/{name}")]
        public IActionResult GetProducts(string name)
        {
            var model = _mapper.Map<IEnumerable<Product>, IEnumerable<SearchProductViewModel>>(_productRepository.GetProductSearch(name));
            return Ok(model);
        }

        private string SaveAnImages(ImageProductViewModel imageProductViewModels)
        {

            string imageString = imageProductViewModels.Value.Split(";base64,")[1];
            byte[] array = Convert.FromBase64String(imageString);
            ImageConverter converter = new ImageConverter();
            Image image = (Image)converter.ConvertFrom(array);
            string NewName = Guid.NewGuid().ToString() + ".png";
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", NewName);
            image.Save(fullPath);
            return NewName;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            string id = User.GetUserIdToken();
            var AllProducts = _userProductRepository.GetUserProductsWithImages(id);
            return Ok(AllProducts);
        }

        [HttpGet]
        [Route("GetUserProduct")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserProducts()
        {
            string userId = User.GetUserIdToken();
            var model = _mapper.Map<IEnumerable<UserProduct>, IEnumerable<UserProductViewModel>>
                  (_userProductRepository.GetUserProductByID(userId));

            return Ok(model);
        }

        [HttpGet]
        [Route("GetUserProduct/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserProducts(string id)
        {

            var model = _mapper.Map<IEnumerable<UserProduct>, IEnumerable<UserProductViewModel>>
                  (_userProductRepository.GetUserProductByID(id));

            return Ok(model);
        }
        [HttpGet(template: "{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var model = _mapper.Map<UserProduct, UserProductViewModel>
                (_userProductRepository.GetProductByID(productId));
            return Ok(model);
        }

        [HttpGet]
        [Route("GetProductDetails/{id}")]
        public IActionResult GetProductDetails(int Id)
        {
            var productDetails = _userProductRepository.GetUserProductDetails(Id);
            return Ok(productDetails);
        }
        [HttpGet]
        [Route("GetNumberOfSoldItems/{id}")]
        public IActionResult GetNumberOfSoldItems(string Id)
        {
            var soldItem = _userProductRepository.GetNumberOfSoldItems(Id);
            return Ok(soldItem);
        }
    }
}