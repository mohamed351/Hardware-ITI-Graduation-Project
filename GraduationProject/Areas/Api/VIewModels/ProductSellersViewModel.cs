using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class ProductSellersViewModel
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Condition { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsReviewed { get; set; }
    }
}
