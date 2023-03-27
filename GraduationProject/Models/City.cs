using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Models
{
    public class City
    {
        public int ID { get; set; }
        [Required]
        public string CityName { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
