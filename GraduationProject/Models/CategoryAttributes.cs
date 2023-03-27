using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class CategoryAttributes
    {
        public int CategoryId { get; set; }
        public int CategoryAttributeId { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual Attributes CategoryAttribute { get; set; }
    }
}
