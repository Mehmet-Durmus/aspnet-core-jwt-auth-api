using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException(string userName)
            : base($"User name {userName} is already in use!") { }
    }
}
