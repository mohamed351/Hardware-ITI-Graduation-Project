using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class UserProductImages
    {
        public string Images { get; set; }
        public int UserProductId { get; set; }

        public virtual UserProduct UserProduct { get; set; }
    }
}
