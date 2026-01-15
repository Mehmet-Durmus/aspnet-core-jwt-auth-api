using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.DTOs
{
    public class UserIdentifierDto
    {
        public required string UserNameOrEmail { get; set; }
    }
}
