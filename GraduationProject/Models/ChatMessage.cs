using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Models
{
    public class ChatMessages
    {
        public int ID { get; set; }
        [ForeignKey("SourceUser")]
        public string SourceUserId { get; set; }
        public ApplicationUser SourceUser { get; set; }
        [ForeignKey("DestinationUser")]
        public string DestinationUserID { get; set; }
        public ApplicationUser DestinationUser { get; set; }
        public DateTime DateTimeOfMessage { get; set; }

        public string Message { get; set; }
        public bool IsSeen { get; set; }

    }
}
