using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class UsersReviews
    {
        public int ID { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public string UserReviewID { get; set; }
        public string UserReviewName { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
