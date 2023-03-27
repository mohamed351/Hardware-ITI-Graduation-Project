using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Admin.ViewModels
{
    public class ProductModelBrandViewModel
    {
        public ProductModelBrandViewModel()
        {
            this.ProductAttributes = new List<Attributes>();
        }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
        public int BrandId { get; set; }
        public SelectList Brands { get; set; }
        public int ModelId { get; set; }
        public int ProductId { get; set; }
        public List<Attributes> ProductAttributes { get; set; }
        public List<string> ProductValues { get; set; }
    }
}
