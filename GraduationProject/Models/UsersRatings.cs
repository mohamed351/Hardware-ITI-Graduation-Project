using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class UsersRatings
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
