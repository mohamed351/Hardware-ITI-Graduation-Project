using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class AttributeRepositry:Repositry<Attributes, int>,IAttributeRepositry
    {

        public AttributeRepositry(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
