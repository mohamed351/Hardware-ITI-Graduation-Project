using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface ICategoryRepositry:IRepositry<Category,int>
    {
        public Category GetCategoryWithAttributes(int ID);
    }
}
