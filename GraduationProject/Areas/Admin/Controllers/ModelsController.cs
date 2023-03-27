using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ModelsController : Controller
    {
        public IModelRepositry Repositry { get; }
        public ICategoryRepositry CategoryRepo { get; }

        public ModelsController(IModelRepositry repositry, ICategoryRepositry category)
        {
            Repositry = repositry;
            CategoryRepo = category;
        }
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetModals(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];

            return Json(Repositry.GetDataTable(start, length, a => a.Name.Contains(search), a => a.Id));
        }

       
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Models/Create
        public ActionResult Create()
        {
           
           
            ViewBag.CategoryId = new SelectList(CategoryRepo.GetAll(), "Id", "Name");
            return PartialView("Create");
        }

        // POST: Models/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repositry.Add(collection);
                    Repositry.SaveAll();
                    return Ok("The Model has been Created");
                }
                return BadRequest("The Model is Not Valid");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

        // GET: Models/Edit/5
        public ActionResult Edit(int id)
        {
           Model model =  Repositry.Get(id);
            ViewBag.CategoryId = new SelectList(CategoryRepo.GetAll(), "Id", "Name");
            return PartialView("Edit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Model collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.Repositry.Edit(collection);
                    this.Repositry.SaveAll();
                    return Ok("The Model has been Changed");
                }
                return BadRequest("An Error Has Occurred.");

                
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

      
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
              Model model =    Repositry.Get(id);
                if(model == null)
                {
                    return NotFound("The Model is not found");
                }
                this.Repositry.Delete(model);
                this.Repositry.SaveAll();
                return Ok("The Model has been deleted");




            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }
    }
}