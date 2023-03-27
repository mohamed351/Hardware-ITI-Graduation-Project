using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class ReviewRepository:Repositry<UsersReviews,int> , IReviewRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<UsersReviews> GetReviewsByUser(string userID)
        {
            return dbContext.UsersReviews.Where(a => a.UserId == userID).ToList();
        }

    }
}
