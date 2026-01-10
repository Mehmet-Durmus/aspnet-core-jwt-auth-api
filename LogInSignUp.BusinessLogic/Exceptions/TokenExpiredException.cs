using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException()
            : base("Token has expired.") { }
    }
}
