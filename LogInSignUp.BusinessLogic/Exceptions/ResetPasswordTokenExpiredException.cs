using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class ResetPasswordTokenExpiredException : AppException
    {
        public ResetPasswordTokenExpiredException()
            :base(410, "Reset password token has expired.") {}
    }
}
