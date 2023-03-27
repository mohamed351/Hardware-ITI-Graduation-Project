using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IReviewRepository :IRepositry<UsersReviews,int>
    {
        public IEnumerable<UsersReviews> GetReviewsByUser(string userID);

    }
}
