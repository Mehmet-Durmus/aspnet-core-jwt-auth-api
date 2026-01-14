using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class RefreshTokenExpiredException : AppException
    {
        public RefreshTokenExpiredException()
            :base(401, "Refresh token has expired.") {}
    }
}
