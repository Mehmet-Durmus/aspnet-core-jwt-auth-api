using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Token
{
    public sealed class EmailVerificationSettings
    {
        public int TokenLifetimeMinutes { get; set; }
        public int ResendCooldownSeconds { get; set;}
    }
}
