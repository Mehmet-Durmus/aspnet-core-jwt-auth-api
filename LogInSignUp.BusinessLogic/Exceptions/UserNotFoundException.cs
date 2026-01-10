using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("The requested user could not be found.") { }
    }
}
