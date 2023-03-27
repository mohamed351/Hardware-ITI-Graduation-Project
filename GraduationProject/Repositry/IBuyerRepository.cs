using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public interface IBuyerRepository:IRepositry<Buys,int>
    {
        public UserProduct GetProductBuyers(int ProductId);

        public void UserSold(int productId, string userID);
        public bool IsProductOwner(int productId, string UserID);
        public void ReviewAndRating(string DestaitionUser, string UserID, string Review, int Ratring);





    }
}
