using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationProject.Areas.Api.VIewModels;
using GraduationProject.ExtenstionMethods;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public CitiesController(IMapper mapper, ICitiesRepositry citiesRepositry)
        {
            Mapper = mapper;
            CitiesRepositry = citiesRepositry;
        }

        public IMapper Mapper { get; }
        public ICitiesRepositry CitiesRepositry { get; }

        [HttpGet]
        public ActionResult<IEnumerable<CitiesViewModel>> GetCities()
        {

            return Ok(Mapper.Map<IEnumerable<City>,IEnumerable<CitiesViewModel>>(CitiesRepositry.GetAll()));
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("testing")]
        public ActionResult TestingGetID()
        {
           string id = User.GetUserIdToken();
            return Ok(id);
        }

    }
}