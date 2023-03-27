using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class SuggestionRepository: Repositry<Suggestions, int>, ISuggestionRepository
    {
        public SuggestionRepository(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
