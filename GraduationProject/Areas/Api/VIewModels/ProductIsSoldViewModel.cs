using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class ProductIsSoldViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public int ProductID { get; set; }

        public string Review { get; set; }

        public int Rating { get; set; }
    }
}
