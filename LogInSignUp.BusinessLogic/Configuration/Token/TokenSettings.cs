using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Token
{
    public sealed class TokenSettings
    {
        public EmailVerificationTokenSettings EmailVerificationSettings { get; set; } = null!;
        public JwtSettings JwtSettings { get; set; } = null!;
        public RefreshTokenSettings RefreshTokenSettings { get; set; } = null!;
        public ResetPasswordTokenSettings ResetPasswordSettings { get; set; } = null!;
    }
}
