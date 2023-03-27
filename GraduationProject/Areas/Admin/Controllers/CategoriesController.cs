using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Areas.Admin.ViewModels;

using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepositry category;

        public IAttributeRepositry Attribute { get; }
      

        public CategoriesController(IAttributeRepositry attribute, ICategoryRepositry category)
        {
            Attribute = attribute;
            this.category = category;
        }
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCategories(int start = 0, int length = 10)
        {
           
            string search = HttpContext.Request.Query["search[value]"];

            return Json(category.GetDataTable(start, length, a => a.Name.Contains(search), a => a.Id));
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
         
            ViewBag.GetAttributes = new SelectList(Attribute.GetAll(),"Id","Name");
            return PartialView("Create");
        }

        // POST: Categories/Create
        [HttpPost]
       
        public ActionResult Create([FromBody]CategoryCreateViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category newCategory = new Category()
                    {
                        Name = collection.CategoryName,

                    };
                    foreach (var item in collection.AttributesID)
                    {
                        newCategory.CategoryAttributes.Add(new CategoryAttributes() { 
                            CategoryAttributeId =item

                        });
                    }


                    category.Add(newCategory);
                    category.SaveAll();
                    
                    return Ok("Category has been Added");
                }

                return BadRequest("Categoty Data is not valid");
            }
            catch
            {
                return BadRequest("Categoty Data is not valid");
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
          
           Category category =  this.category.GetCategoryWithAttributes(id);
            ViewBag.GetAttributes = this.Attribute.GetAll();
            return PartialView("Edit",category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        public ActionResult Edit([FromBody] CategoryEditViewModel categoryEditView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = this.category.GetCategoryWithAttributes(categoryEditView.ID);
                    category.Name = categoryEditView.CategoryName;
                    category.CategoryAttributes.Clear();
                    foreach (var item in categoryEditView.AttributesID)
                    {
                        category.CategoryAttributes.Add(new CategoryAttributes()
                        {
                            CategoryAttributeId = item
                        });
                    }
                    this.category.Edit(category);
                    this.category.SaveAll();
                    return Ok("Category has been changed");

                }

                return Ok("Category data is not valid");
            }
            catch
            {
                return BadRequest("Category Data is not valid");
            }
        }

      

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
               Category category1 =   category.GetCategoryWithAttributes(id);

                if(category1 == null)
                {
                    return NotFound("The Category is not found");
                }
                category1.CategoryAttributes.Clear();
                category.Delete(category1);
                category.SaveAll();
                return Ok("The Category has been Deleted");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }
        
    }
    
}