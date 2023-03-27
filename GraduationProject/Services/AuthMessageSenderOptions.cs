using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        //sendGrid key sent to me on creation of an API key by SendGrid account
        public string SendGridKey { get; set; }
    }
}
