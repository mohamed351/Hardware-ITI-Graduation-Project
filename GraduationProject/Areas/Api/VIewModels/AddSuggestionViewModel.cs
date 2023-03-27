using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.VIewModels
{
    public class AddSuggestionViewModel
    {
        [Required]
        public string SuggestedModel { get; set; }
        public string Description { get; set; }
    }
}
