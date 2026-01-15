using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.DTOs
{
    public class RefreshTokenLogInDto
    {
        public string UserId { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
