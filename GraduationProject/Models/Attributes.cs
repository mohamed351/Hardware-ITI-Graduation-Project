using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public partial class Attributes
    {
        public Attributes()
        {
            CategoryAttributes = new HashSet<CategoryAttributes>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CategoryAttributes> CategoryAttributes { get; set; }
    }
}
