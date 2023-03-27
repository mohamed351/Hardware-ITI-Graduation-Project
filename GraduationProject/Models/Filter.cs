using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Models
{
    public class Filter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilterId { get; set; }
        public string Name { get; set; }
        public string FilterType { get; set; }
        public string QueryKey { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } //IColllectionOf 
        public ICollection<Filter_FilterChoice> filter_FilterChoices { get; set; }
        public ICollection<DynamicChoice> dynamicChoices { get; set; }
    }
    public class FilterChoice
    {
        public int ID { get; set; }
        public string Choice { get; set; }
        public ICollection<Filter_FilterChoice> filter_FilterChoices { get; set; }
    }
    public class Filter_FilterChoice
    {
        public int ID { get; set; }
        public Filter Filter { get; set; }
        [ForeignKey("Filter")]
        public int FilterId { get; set; }
        public FilterChoice FilterChoice { get; set; }
        [ForeignKey("FilterChoice")]
        public int FilterChoiceId { get; set; }
    }
    public class DynamicChoice
    {
        public int ID { get; set; }
        public string Procedure { get; set; }
        public Filter Filter { get; set; }
        [ForeignKey("Filter")]
        public int FilterId { get; set; }
    }
}
