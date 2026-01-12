using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Token
{
    public sealed class TokenSettings
    {
        public EmailVerificationSettings EmailVerificationSettings { get; set; } = null!;
        public JwtSettings JwtSettings { get; set; } = null!;
        public RefreshTokenSettings RefreshTokenSettings { get; set; } = null!;
    }
}
