using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.ViewModels
{
    public class ChatMessageViewModel
    {
        public string DestinationID { get; set; }
        public string DestinationName { get; set; }

        public string SourceID { get; set; }
        public string SourceName { get; set; }

        public string Message { get; set; }

        public DateTime messageTime { get; set; }
    }
}
