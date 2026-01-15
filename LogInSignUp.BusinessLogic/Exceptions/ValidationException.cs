using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class ValidationException : AppException
    {
        public string[] Errors { get; set; }
        public ValidationException(IEnumerable<string> errors) 
            : base(StatusCodes.Status400BadRequest) 
        {
            Errors = errors.ToArray();
        }
    }
}
