using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IProductRepository: IRepositry<Product, int>
    {
        public IEnumerable<Model> GetModelWithCategories(int CategoryId);
        public IEnumerable<Attributes> GetCategoryAttributes(int CategoryId);
        public IEnumerable<Product> GetProductSearch(string name);
    }
}
