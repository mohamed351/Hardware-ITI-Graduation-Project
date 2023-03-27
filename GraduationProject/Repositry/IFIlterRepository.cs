using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IFIlterRepository : IRepositry<Filter, int>
    {
        public List<Dictionary<string, Object>> GetFilterByCategory(int ID);
        public List<Brand> GetBrandsByCategory(int CategoryId);
        public List<FilterChoice> GetStaticChoicesByCategory(int categoryId);
        public List<UserProduct> GetFilterdProducts(string query);
    }
}
