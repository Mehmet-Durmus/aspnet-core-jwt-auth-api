using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class ResetPasswordAlreadyRequestedException : AppException
    {
        public ResetPasswordAlreadyRequestedException()
            : base(409, "An active password reset request already exists.") {}
    }
}
