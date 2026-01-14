using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public sealed class EmailAlreadyExistsException : AppException
    {
        public EmailAlreadyExistsException(string email)
            : base(409, $"Email address {email} is already in use!") {}
    }
}
