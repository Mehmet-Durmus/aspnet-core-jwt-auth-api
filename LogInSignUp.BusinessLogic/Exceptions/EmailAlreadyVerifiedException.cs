using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class EmailAlreadyVerifiedException : Exception
    {
        public EmailAlreadyVerifiedException()
            : base($"Email address is already verified.") { }
    }
}
