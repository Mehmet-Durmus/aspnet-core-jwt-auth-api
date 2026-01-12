using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Mail
{
    public sealed class MailSettings
    {
        public EmailVerificationMailSettings EmailVerificationSettings { get; set; } = null!;
        public ResetPasswordMailSettings ResetPasswordSettings { get; set; } = null!;
    }
}
