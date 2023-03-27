using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IUsersRepository : IRepositry<ApplicationUser,string>
    {
        ApplicationUser GetUserInformation(string UserId);
         double GetAverageRating(string UserId);


    }
}
