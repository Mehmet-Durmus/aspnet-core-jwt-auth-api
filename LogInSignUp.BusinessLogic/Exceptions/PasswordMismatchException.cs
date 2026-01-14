using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class PasswordMismatchException : AppException
    {
        public PasswordMismatchException()
            : base(400, "Password and password confirmation do not match.")
        {
        }
    }
}
