using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class UserProductRepository : Repositry<UserProduct,int> , IUserProductRepository
    {
        private readonly ApplicationDbContext context;
        public UserProductRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<UserProduct> GetUserProductByID(string UserID)
        {
            return context.UserProduct
                .Include(a=>a.UserProductImages)
                .Where(a => a.UserId == UserID);
        }
        public UserProduct GetProductByID(int ProductId)
        {
            return context.UserProduct
                .Include(a => a.UserProductImages)
                .FirstOrDefault(a => a.Id == ProductId);
                
        }

        public IEnumerable<UserProduct> GetUserProductsWithImages(string id)
        {
            if(id == null)
            {
                return context.UserProduct
                    .Include(a => a.UserProductImages)
                    .Where(a => !a.Buys.Select(b => b.UserProductId).Contains(a.Id)
                    || a.Buys.Any(b => b.IsSold == false))
                    .ToList();
            }

            else
            {
                return context.UserProduct
               .Include(a => a.UserProductImages)
               .Where(a => !a.Buys.Select(b => b.UserProductId).Contains(a.Id)
               || a.Buys.Any(b => b.IsSold == false))
               .Where(a => a.UserId != id)
               .ToList();
            }

        }

        public UserProduct GetUserProductDetails(int userProductId)
        {
            return context.UserProduct
                .Include(a => a.UserProductImages)
                .Include(a => a.User)
                .Include(a => a.User.Area)
                .ThenInclude(a => a.City)
                .Include(a => a.Product)
                .ThenInclude(a => a.ProductAttributes)
                .Include(a => a.Product.Model)
                .Include(a => a.Product.Brand)
                .FirstOrDefault(a => a.Id == userProductId);
        }

        public int GetNumberOfSoldItems(string userId)
        {
            return context.Buys.Count(a => a.UserId == userId);
        }
    }
}
