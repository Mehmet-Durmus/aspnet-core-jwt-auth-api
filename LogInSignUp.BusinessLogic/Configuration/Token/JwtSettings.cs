using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Configuration.Token
{
    public class JwtSettings
    {
        public required string Issuer { get; set; } = null!;
        public required string Audience { get; set; } = null!;
        public required string SecurityKey { get; set; } = null!;
        public required int TokenLifetimeDays { get; set; }
    }
}
