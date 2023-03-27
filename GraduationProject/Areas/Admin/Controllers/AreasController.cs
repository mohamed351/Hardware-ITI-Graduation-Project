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
    public class AreasController : Controller
    {
        public IAreaRepositry AreaRepositry { get; }
        public ICitiesRepositry CitiesRepositry { get; }

        public AreasController(IAreaRepositry areaRepositry, ICitiesRepositry citiesRepositry)
        {
            AreaRepositry = areaRepositry;
            CitiesRepositry = citiesRepositry;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAreas(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
            
            return Json(AreaRepositry.GetDataTable(start, length, a => a.AreaName.Contains(search), a => a.ID));
        }



        // GET: Areas/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
           

            ViewBag.CityID = new SelectList(CitiesRepositry.GetAll(), "ID", "CityName");
            return PartialView("Create");
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Area collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AreaRepositry.Add(collection);
                    AreaRepositry.SaveAll();
                    return Ok("The Area is Added");
                }
                return BadRequest("The Area Data is Complated");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int id)
        {
            Area area = this.AreaRepositry.Get(id);
            if(area == null)
            {
                return NotFound("The Area is not Found");
            }
            ViewBag.CityID = new SelectList(CitiesRepositry.GetAll(), "ID", "CityName");
            return PartialView("Edit", area);
        }

        // POST: Areas/Edit/5
        [HttpPost]
        public ActionResult Edit(Area collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.AreaRepositry.Edit(collection);
                    this.AreaRepositry.SaveAll();

                    return Ok("The Area is Edited");
                }

                return BadRequest("An Error Has Occurred.");

            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }

     
        // POST: Areas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Area area = this.AreaRepositry.Get(id);
                if(area == null)
                {
                    return NotFound("This Area does not Exist");
                }
                this.AreaRepositry.Delete(area);
                this.AreaRepositry.SaveAll();

                return Ok("The Area has been Deleted");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }
    }
}