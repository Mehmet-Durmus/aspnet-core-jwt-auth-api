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
        private readonly EmailVerificationSettings _settings;

        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
            _settings = _mailSettings.EmailVerificationSettings;
        }

        public async Task SendEmailVerificationMail(User user, string emailVerificationToken)
        {
            string url = _settings.BaseUrl.EndsWith("/") ? _settings.BaseUrl[..^1] : _settings.BaseUrl;
            string mailBody = $"{_settings.MailBody}<br><br><strong><a target=\"_blank\" href=\"{url}/verify-email/{user.Id}/{emailVerificationToken}\">Epostanı doğrula</a></strong>";
            //string mailBody = $"<a href=\"https://google.com\">Test</a>";
            await SendMailAsync(user.Email,
                _settings.Subject,
                mailBody,
                _settings.EmailAddress,
                _settings.DisplayName,
                _settings.Password,
                _settings.Port,
                _settings.EnableSsl,
                _settings.Host);
            
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(to);
            mail.Subject = subject; // MailSettings
            mail.Body = body; // MailSettings
            mail.From = new(from, displayName, System.Text.Encoding.UTF8); // MailSettings

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(from, password); // "ixzygzhozgwyczoe"
            smtp.Port = port; // MailSettings
            smtp.EnableSsl = enableSSl;
            smtp.Host = host; // MailSettings
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string to, string subject, string body, string from, string displayName, string password, int port, bool enableSSl, string host, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, from, displayName, password, port, enableSSl, host, isBodyHtml);
        }
    }
}
