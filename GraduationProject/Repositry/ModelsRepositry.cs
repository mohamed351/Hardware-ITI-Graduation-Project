using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class ModelsRepositry:Repositry<Model,int>, IModelRepositry
    {
        public ModelsRepositry(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
