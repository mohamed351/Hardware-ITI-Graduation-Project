using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class BrandRepository : Repositry<Brand, int>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context)
         : base(context)
        {
        }
    }
}
