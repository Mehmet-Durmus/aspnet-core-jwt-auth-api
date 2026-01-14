using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class UserNameAlreadyExistsException : AppException
    {
        public UserNameAlreadyExistsException(string userName)
            : base(409, $"User name {userName} is already in use!") { }
    }
}
