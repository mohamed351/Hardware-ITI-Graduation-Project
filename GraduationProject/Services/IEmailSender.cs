using System.Threading.Tasks;

namespace GraduationProject.Services
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);

    }
}