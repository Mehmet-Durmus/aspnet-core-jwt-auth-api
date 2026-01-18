using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class EmailVerificationTokenExpiredException : AppException
    {
        public EmailVerificationTokenExpiredException()
            :base(400, "Invalid or expired token.") {}
    }
}
