using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AttributesController : Controller
    {
        public IAttributeRepositry AttributeRepositry { get; }

        public AttributesController(IAttributeRepositry attributeRepositry)
        {
            AttributeRepositry = attributeRepositry;
        }
  
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult GetAttribute(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
          
            return Json(AttributeRepositry.GetDataTable(start, length, a => a.Name.Contains(search), a => a.Id));
        }



        public ActionResult Details(int id)
        {
            return View();
        }

      
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attributes colllection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AttributeRepositry.Add(colllection);
                    AttributeRepositry.SaveAll();
                    return Ok("The Attribute has been Added");
                }
                return BadRequest("The Attribute Data is not Valid");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

       
        public ActionResult Edit(int ? id)
        {

            if(id == null)
            {
                return BadRequest("The Request is not Valid");
            }
            Attributes attributes = this.AttributeRepositry.Get(id.Value);
            if(attributes == null)
            {
                return NotFound("Attribute Not Found.");
            }


            return PartialView("Edit", attributes);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Attributes attributes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.AttributeRepositry.Edit(attributes);
                    this.AttributeRepositry.SaveAll();
                    return Ok("The Attribute has been edited");
                }
                return BadRequest("An Error Has Occurred.");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

      

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
               
                 Attributes attributes =  this.AttributeRepositry.Get(Id);
                this.AttributeRepositry.Delete(attributes);
                this.AttributeRepositry.SaveAll();
                return Ok("The Attribute has been Deleted");
            }
            catch
            {
              return Ok("An Error Has Occurred.");
            }
        }
    }
}