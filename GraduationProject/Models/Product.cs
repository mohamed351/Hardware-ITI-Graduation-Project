using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class Product
    {
        public Product()
        {
            UserProduct = new HashSet<UserProduct>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Name { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
        public virtual ICollection<UserProduct> UserProduct { get; set; }
        public ICollection<ProductAttributes> ProductAttributes { get; set; }
    }
}
