using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class EmailNotVerifiedException : AppException
    {
        public EmailNotVerifiedException()
            : base(403, "Email address has not been verified.") {}
    }
}
