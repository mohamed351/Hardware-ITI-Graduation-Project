using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryAttributes = new HashSet<CategoryAttributes>();
            Model = new HashSet<Model>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryAttributes> CategoryAttributes { get; set; }
        public virtual ICollection<Model> Model { get; set; }
        public ICollection<Filter> Filters { get; set; }
    }
}
