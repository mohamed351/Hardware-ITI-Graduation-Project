using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class ProductAttributesRepository: Repositry<ProductAttributes, int>, IProductAttributesRepository
    {
        private readonly ApplicationDbContext context;
        public ProductAttributesRepository(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }
        public IEnumerable<ProductAttributes> GetProductAttributes(int productId)
        {
            return context.ProductAttributes.Where(a => a.ProductId == productId).ToList();
        }
    }
}
