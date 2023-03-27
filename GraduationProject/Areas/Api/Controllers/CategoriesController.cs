using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Repositry;
using GraduationProject.Models;
using GraduationProject.Areas.Api.VIewModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepositry _CategoryRepository;
        private readonly IFIlterRepository _FIlterRepository;

        public CategoriesController(ICategoryRepositry categoryRepositry, IFIlterRepository fIlterRepository)
        {
            _CategoryRepository = categoryRepositry;
            _FIlterRepository = fIlterRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var AllCategories = _CategoryRepository.GetAll();
            return Ok(AllCategories);
        }

        [HttpGet]
        [Route("GetCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            //int i = int.Parse(id);
            Category category = _CategoryRepository.Get(id);
            List<Dictionary<string, Object>> filters = _FIlterRepository.GetFilterByCategory(id);
           // List<string> titleNames= _FIlterRepository.GetFilterByCategory(id);
            //List<string> StaticChoices= _FIlterRepository.GetStaticChoices(id);
            //List<string> dynamicChoices= _FIlterRepository.GetDynamicChoices(id);
            return Ok(filters);
        }

        [HttpGet]
        [Route("GetFilterProducts")]
        public IActionResult GetFilterProducts([FromQuery] FilterProductViewModel filterProductViewModel)
        {
            string method = " SELECT p.* FROM UserProduct p ";
            string innerJoin = " INNER JOIN Product g ON g.Id = p.ProductId " +
                                " INNER JOIN Model m ON m.Id = g.ModelId " +
                                " INNER JOIN Category c ON c.Id = m.CategoryId ";
            string whereClause = "";
            if (filterProductViewModel.Brand != null && filterProductViewModel.Brand.Count != 0)
            {
                innerJoin += " INNER JOIN dbo.Brand b " +
                                  " ON b.Id = g.BrandId ";

                whereClause += " (b.Name='" + filterProductViewModel.Brand[0] + "' ";
                for (int i = 1; i <=  filterProductViewModel.Brand.Count(); i++)
                {

                    whereClause += " OR b.Name='" + filterProductViewModel.Brand[i-1] + "' ";
                }
                whereClause += ")";
            }
            if (filterProductViewModel.Rating != 0)
            {
                innerJoin += " LEFT JOIN Users_Ratings r " +
                             " ON r.UserId = p.UserId ";
                if(whereClause.Length != 0)
                {
                    whereClause += " AND ";
                }
                whereClause += " (r.Rating >= " + filterProductViewModel.Rating + " OR r.Rating is NULL)";
            }

            if (filterProductViewModel.Condition != null && filterProductViewModel.Condition.Count != 0) 
            {
                if (whereClause.Length != 0)
                {
                    whereClause += " AND ";
                }
                //method = method + " where p.condition =";
                whereClause += " (";
                for (int i = 0; i < filterProductViewModel.Condition.Count(); i++)
                {
                    if(i != 0)
                    {
                        whereClause += " OR ";
                    }
                    whereClause += " p.condition=";
                    if (filterProductViewModel.Condition[i] == "New")
                    {
                        whereClause += " 0 ";
                    }
                    if (filterProductViewModel.Condition[i] == "Used with Box")
                    {
                        whereClause += " 1 ";
                    }
                    if (filterProductViewModel.Condition[i] == "Used without Box")
                    {
                        whereClause += " 2 ";
                    }
                }
                whereClause += " ) ";
            }

            if (filterProductViewModel.FromPrice != 0)
            {
                if (whereClause.Length != 0)
                {
                    whereClause += " AND ";
                }
                whereClause += "p.price >" + filterProductViewModel.FromPrice;

            }


            if (filterProductViewModel.ToPrice != 0)
            {
                if (whereClause.Length != 0)
                {
                    whereClause += " AND ";
                }
                whereClause += "p.price <" + filterProductViewModel.ToPrice;

            }

            if (whereClause.Length != 0)
            {
                whereClause += " AND ";
            }

            whereClause += " c.Id= " + filterProductViewModel.CategoryId;

            whereClause = " WHERE " + whereClause;

            method += innerJoin + whereClause;
 
            List<UserProduct> products = _FIlterRepository.GetFilterdProducts(method);

            return Ok(products);
        }
    }
}