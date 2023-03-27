using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CitiesController : Controller
    {
        public ICitiesRepositry CitiesRepositry { get; }
        public CitiesController(ICitiesRepositry citiesRepositry)
        {
            CitiesRepositry = citiesRepositry;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetCities(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];

            return Json(CitiesRepositry.GetDataTable(start, length, a => a.CityName.Contains(search), a => a.ID));
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
        public ActionResult Create(City city)
        {

            if (ModelState.IsValid)
            {
                this.CitiesRepositry.Add(city);
                this.CitiesRepositry.SaveAll();
                return Ok("The City has been Added");

            }
            return BadRequest("The Model is Valid");
           
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return BadRequest("The ID is not Found");
            }
           City city = this.CitiesRepositry.Get(id.Value);
            if(city == null)
            {
                return NotFound("The Employee is not found");
            }

            return PartialView("Edit", city);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.CitiesRepositry.Edit(city);
                    this.CitiesRepositry.SaveAll();
                    return Ok("The City Data has Changed");
                }
                return BadRequest("The City data is not valid");
               
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

               City city = CitiesRepositry.Get(id);
                CitiesRepositry.Delete(city);
                CitiesRepositry.SaveAll();
                return Ok("The City has been deleted");
            }
            catch
            {
                return BadRequest("An Error Has Occurred, The City has Areas Please Delete the Included Areas First.");
            }
        }
    }
}