using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class CitiesRepositry:Repositry<City,int>, ICitiesRepositry
    {

        public CitiesRepositry(ApplicationDbContext context)
            :base(context)

        {

        }
    }
}
