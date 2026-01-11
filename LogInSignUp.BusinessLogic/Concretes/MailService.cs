using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.Configuration.Mail;
using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Concretes
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly EmailVerificationMailSettings _emailVerificationSettings;

        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
            _emailVerificationSettings = _mailSettings.EmailVerificationSettings;
        }

        public async Task SendEmailVerificationMail(User user, string emailVerificationToken)
        {
            string url = _emailVerificationSettings.BaseUrl.EndsWith("/") ? _emailVerificationSettings.BaseUrl[..^1] : _emailVerificationSettings.BaseUrl;
            string mailBody = $"{_emailVerificationSettings.MailBody}<br><br><strong><a target=\"_blank\" href=\"{url}/verify-email/{user.Id}/{emailVerificationToken}\">Epostanı doğrula</a></strong>";
            await SendMailAsync(user.Email,
                _emailVerificationSettings.Subject,
                mailBody,
                _emailVerificationSettings.EmailAddress,
                _emailVerificationSettings.DisplayName,
                _emailVerificationSettings.Password,
                _emailVerificationSettings.Port,
                _emailVerificationSettings.EnableSsl,
                _emailVerificationSettings.Host);
            
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body; 
            mail.From = new(from, displayName, System.Text.Encoding.UTF8); 

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(from, password); 
            smtp.Port = port;
            smtp.EnableSsl = enableSSl;
            smtp.Host = host;
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string to, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, from, displayName, password, port, enableSSl, host, isBodyHtml);
        }
    }
}
