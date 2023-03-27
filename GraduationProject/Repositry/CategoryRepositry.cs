using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class CategoryRepositry:Repositry<Category,int>,ICategoryRepositry
    {
        private readonly ApplicationDbContext context;

        public CategoryRepositry(ApplicationDbContext context)
            :base(context)
        {
            this.context = context;
        }

        public Category GetCategoryWithAttributes(int ID)
        {
            return context.Category
                .Include(a => a.CategoryAttributes)
                .ThenInclude(a => a.CategoryAttribute)
                .FirstOrDefault(a=>a.Id == ID);
        }
    }
}
