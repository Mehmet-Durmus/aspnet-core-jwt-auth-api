using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class ResetPasswordTokenAlreadyUsedException : AppException
    {
        public ResetPasswordTokenAlreadyUsedException()
           : base(400, "The token has already been used.") { }
    }
}
