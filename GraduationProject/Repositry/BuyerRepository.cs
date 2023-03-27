using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GraduationProject.Repositry
{  
    public class BuyerRepository : Repositry<Buys, int>, IBuyerRepository
    {
        private readonly ApplicationDbContext context;

        public BuyerRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public UserProduct GetProductBuyers(int ProductId)
        {
            return context.UserProduct
                 .Include(a=>a.Buys)
                 .ThenInclude(a=>a.User)
                 .FirstOrDefault(a=>a.Id == ProductId);
                
                
        }

        public void UserSold(int productId, string userID)
        {
           
          var buyer =  context
                .Buys
                .FirstOrDefault(a => a.UserProductId == productId && a.UserId == userID);
            if (buyer != null)
            {

                buyer.IsSold = true;
            }
            else
            {
                buyer = context.Buys.FirstOrDefault(a => a.UserProductId == productId && a.IsSold == true);
                buyer.IsReview = true;
            }
            

        }

        public bool IsProductOwner(int productId, string UserID)
        {
            bool IsOwner = false;
            var product = context.UserProduct.FirstOrDefault(a => a.Id == productId);
            if(product != null)
              IsOwner = product.UserId == UserID ? true : false;
            
            return IsOwner;
           
        }

        public void ReviewAndRating(string DestaitionUser, string UserID, string Review, int Ratring)
         {

           ApplicationUser Sourceuser =  context.Users.FirstOrDefault(a => a.Id == UserID);
            ApplicationUser DestaintionUser = context.Users.FirstOrDefault(a => a.Id == DestaitionUser);
            DestaintionUser.UsersRatings = new List<UsersRatings>();
             DestaintionUser.UsersRatings.Add(new UsersRatings()
            {
                Rating = Ratring,
                UserId = Sourceuser.Id
            });
            DestaintionUser.UsersReviews = new List<UsersReviews>();
            DestaintionUser.UsersReviews.Add(new UsersReviews()
            {
                Review = Review,
                UserReviewName = Sourceuser.FirstName + " " + Sourceuser.LastName,
                UserId = DestaintionUser.Id,
                UserReviewID = Sourceuser.Id
            });
        }
    }
}
