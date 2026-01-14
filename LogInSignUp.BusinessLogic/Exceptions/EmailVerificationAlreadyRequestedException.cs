using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class EmailVerificationAlreadyRequestedException : AppException
    {
        public EmailVerificationAlreadyRequestedException()
            : base(409, "An active email verification request already exists.") { }
    }
}
