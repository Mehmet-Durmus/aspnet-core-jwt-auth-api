using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException()
            : base("Token is invalid.") { }
        public InvalidTokenException(string message)
            : base(message) { }

    }
}
