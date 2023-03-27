using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(16,MinimumLength = 7)]
        public string Phonenumber { get; set; }

        [Required]
        public int AreaID { get; set; }

        public SelectList Areas { get; set; }
    }
}
