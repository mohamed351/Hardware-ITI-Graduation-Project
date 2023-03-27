using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class SearchProductViewModel
    {
        public SearchProductViewModel()
        {
            Attributes = new List<ProductAttributesViewModel>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public ICollection<ProductAttributesViewModel>  Attributes { get; set; }


    }
    public class ProductAttributesViewModel
    {
        public string AttributeName { get; set; }
        public string Value { get; set; }
      

    }
}
