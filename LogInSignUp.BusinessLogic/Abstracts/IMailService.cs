using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Abstracts
{
    public interface IMailService
    {
        Task SendMailAsync(string[] tos, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true);
        Task SendMailAsync(string to, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true);
        Task SendEmailVerificationMailAsync(string email, string emailVerificationToken);
        Task SendResetPasswordMailAsync(string email, string resetPasswordToken);
    }
}
