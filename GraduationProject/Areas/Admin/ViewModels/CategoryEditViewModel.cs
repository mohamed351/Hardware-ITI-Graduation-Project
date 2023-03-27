using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Admin.ViewModels
{
    public class CategoryEditViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("CategoryName")]
        [Required]
        public string CategoryName { get; set; }
        [JsonProperty("AttributesID")]
        public ICollection<int> AttributesID { get; set; }
    }
}
