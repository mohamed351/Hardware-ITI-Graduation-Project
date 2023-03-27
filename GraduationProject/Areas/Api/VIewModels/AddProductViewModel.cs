using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required,Range(0,1000000)]
        public decimal Price { get; set; }
        [Required]
        public int Condition { get; set; }
        [Required]
        public int ProductId { get; set; }   
        [Required]
        public DateTime DateAdded { get; set; } 
        [Required]
        public bool IsNegotiable { get; set; }     
        [Required]
        public bool IsReplacable{ get; set; }
        [Required]
        public ICollection<ImageProductViewModel> Images { get; set; }
    }
    public class ImageProductViewModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
