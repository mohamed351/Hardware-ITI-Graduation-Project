using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            Buys = new HashSet<Buys>();
        
            Suggestions = new HashSet<Suggestions>();
            UserProduct = new HashSet<UserProduct>();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [ForeignKey("Area")]
        public int AreaID { get; set; }

        public Area Area { get; set; }

        public string Address { get; set; }

        public virtual ICollection<UsersRatings> UsersRatings { get; set; }
        public virtual ICollection<UsersReviews> UsersReviews { get; set; }
        public virtual ICollection<Buys> Buys { get; set; }
        [InverseProperty("DestinationUser")]
        public ICollection<ChatMessages> DestinationUser { get; set; }
        [InverseProperty("SourceUser")]
        public ICollection<ChatMessages> SourceUser { get; set; }


        public virtual ICollection<Suggestions> Suggestions { get; set; }
        public virtual ICollection<UserProduct> UserProduct { get; set; }
    }

}
