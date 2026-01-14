using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class InvalidRefreshTokenException : AppException
    {
        public InvalidRefreshTokenException()
            : base(401, "Refresh token is invalid.") {}
    }
}
