using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Options;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GraduationProject.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        // you can find sendGrid key and user withen the secret manager in secrets.json file
        public AuthMessageSenderOptions Options { get; }

        // Generic function to send mail to any user
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        private async Task Execute(string apiKey, string subject, string message, string receiverEmail)
        {
            // Through sendGrid Key the SMTP of SendGrid will be able to determine the client  
            var client = new SendGridClient(apiKey);
            // Message creation
            var to = new EmailAddress(receiverEmail);
            var From = new EmailAddress("nardeen.karam@gmail.com", Options.SendGridUser);
            //var Subject = subject;
            var plainTextContent = "";
            var htmlContent = message;

           // SendGridMessage msg = new SendGridMessage();
            var msg = MailHelper.CreateSingleEmail(From, to, subject, plainTextContent, htmlContent);
            //msg.AddTo(new EmailAddress(receiverEmail));
            
            //msg.SetFrom(From);
            //msg.ReplyTo = to;
            //msg.SetSubject(subject);
            //msg.SetTemplateData(message);
            //msg.AddTo(to);
           // var button = "<table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed' width='100%' data-muid='bKHWQMgPkL5opYCkxiM6aS'>" +
                //"<tbody><tr><td align='center' class='outer-td' style='padding: 20px 0px 0px 0px;' bgcolor=''>"+
                //"<table border='0' cellpadding='0' cellspacing='0' class='button-css__deep-table___2OZyb wrapper-mobile' style='text-align:center'>"+
                //"<tbody><tr><td align='center' bgcolor='#993300' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'>" +
                //"<a id='ResetBtn'style='background-color:#222222; border:1px solid #993300; border-color:#993300; border-radius:0px; border-width:1px; color:#ffffff; display:inline-block; font-family:verdana,geneva,sans-serif; font-size:16px; font-weight:normal; letter-spacing:1px; line-height:30px; padding:12px 20px 12px 20px; text-align:center; text-decoration:none; border-style:solid;'" +
                //" href='' target='_blank'>Reset Password</a></td></tr></tbody></table></td></tr></tbody></table>'";
            //msg.SetTemplateData(button);
           // msg.SetSpamCheck(true);
            //msg.SetTemplateId("d-714946c7fcc647f295ecb836daf7d3ca");
            

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
