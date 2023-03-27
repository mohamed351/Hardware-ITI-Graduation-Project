using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationProject.Areas.Api.VIewModels;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        public IMapper Mapper { get; }
        public IAreaRepositry AreaRepositry { get; }
        public AreasController(IMapper mapper,IAreaRepositry areaRepositry)
        {
            Mapper = mapper;
            AreaRepositry = areaRepositry;
        }

     

        [HttpGet]
       public ActionResult<IEnumerable<AreasViewModel>> GetAreas([FromQuery]int? ID)
        {
            if(ID == null)
            {
                return BadRequest();
            }
            
           
            return Ok(Mapper.Map<IEnumerable<Area>,IEnumerable<AreasViewModel>>(AreaRepositry.GetAreaByCityID(ID.Value)));
        }
    
    }
}