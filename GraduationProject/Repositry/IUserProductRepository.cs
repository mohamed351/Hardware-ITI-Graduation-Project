using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IUserProductRepository : IRepositry<UserProduct, int>
    {
        public IEnumerable<UserProduct> GetUserProductsWithImages(string id);
        public UserProduct GetUserProductDetails(int userProductId);
        public IEnumerable<UserProduct> GetUserProductByID(string UserID);
        public int GetNumberOfSoldItems(string userId);
        public UserProduct GetProductByID(int ProductId);

    }
}
