using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ChartController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetAverage(int id)
        {
          
            var model = context.UserProduct
                .Include(a => a.Product)
                .Where(a => a.ProductId == id)
                .OrderByDescending(a=>a.DateAdded)
                .Take(10)
                .ToList().GroupBy(a => a.ProductId)
                .Select(a => 
                new {ProductId = a.Key, Avrage =a.Average(a=>a.Price), Prices =a.Select(a=>a.Price) , DateTime = a.Select(a=>a.DateAdded)}).FirstOrDefault();
            


            return Ok(model);
        }
        
    }
   
}