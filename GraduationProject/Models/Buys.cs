using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public partial class Buys
    {
        public bool IsSold { get; set; }
        public string UserId { get; set; }
        public int UserProductId { get; set; }
        public bool IsReview { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual UserProduct UserProduct { get; set; }
    }
}
