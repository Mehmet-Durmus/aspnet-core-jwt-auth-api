using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Mail
{
    public sealed class EmailVerificationMailSettings
    {
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string MailBody { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Host { get; set; } = null!;
    }
}
