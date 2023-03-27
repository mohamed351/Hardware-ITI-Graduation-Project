using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.VIewModels
{
    public class FilterProductViewModel
    {
        public FilterProductViewModel()
        {
            this.Brand = new List<string>();
            this.Condition = new List<string>();
        }
      
        public List<string> Brand{ get; set; }
        public List<string> Condition{ get; set; }
        public int  FromPrice{ get; set; }
        public int ToPrice{ get; set; }
        public int Rating{ get; set; }
        public int CategoryId{ get; set; }
    }
}
