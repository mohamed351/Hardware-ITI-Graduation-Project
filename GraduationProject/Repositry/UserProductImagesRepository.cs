using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class UserProductImagesRepository : Repositry<UserProductImages, int> , IUserProductImagesRepository
    {
        private readonly ApplicationDbContext context;

        public UserProductImagesRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
