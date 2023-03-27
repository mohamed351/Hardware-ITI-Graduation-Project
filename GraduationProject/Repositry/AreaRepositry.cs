using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class AreaRepositry:Repositry<Area,int>, IAreaRepositry
    {
        private readonly ApplicationDbContext context;

        public AreaRepositry(ApplicationDbContext context)
            :base(context)
        {
            this.context = context;
        }

        public IEnumerable<Area> GetAreaByCityID(int ID)
        {
            return context.Areas.Where(a => a.CityID == ID);
        }
    }
}
