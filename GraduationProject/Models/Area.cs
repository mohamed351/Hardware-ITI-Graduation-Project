using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Area
    {
        public int ID { get; set; }
        [Required]
        public string AreaName { get; set; }
        [ForeignKey("CityID")]
        public City City { get; set; }
        [Required]
        public int CityID { get; set; }
        public ICollection<ApplicationUser>  ApplicationUsers { get; set; }
    }
}
