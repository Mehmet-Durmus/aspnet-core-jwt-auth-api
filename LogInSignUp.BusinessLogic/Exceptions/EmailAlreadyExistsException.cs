using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public sealed class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
            : base($"Email address {email} is already in use!") {}
    }
}
