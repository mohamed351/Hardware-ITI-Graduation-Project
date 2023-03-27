using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class UserProductViewModel
    {
        public UserProductViewModel()
        {
            Images = new List<ViewModelUserProductImages>();
        }
        public int UserProductId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Condition { get; set; }
        public string UserId { get; set; }
        public ICollection<ViewModelUserProductImages> Images { get; set; }

    }

    public partial class ViewModelUserProductImages
    {
        public string Image { get; set; }
    }

}
