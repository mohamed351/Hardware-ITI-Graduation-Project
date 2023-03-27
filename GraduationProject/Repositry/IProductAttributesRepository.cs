using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IProductAttributesRepository: IRepositry<ProductAttributes, int>
    {
        public IEnumerable<ProductAttributes> GetProductAttributes(int productId);
    }
}
