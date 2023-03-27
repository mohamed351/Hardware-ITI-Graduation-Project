using System;
using System.Collections.Generic;

namespace GraduationProject.Models
{
    public partial class Suggestions
    {
        public int Id { get; set; }
        public string SuggestedModel { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
