using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Admin.ViewModels
{
    public class CategoryCreateViewModel
    {
        [JsonProperty("CategoryName")]
        [Required]
        public string CategoryName { get; set; }
        [JsonProperty("AttributesID")]
        public ICollection<int> AttributesID { get; set; }
    }
}
