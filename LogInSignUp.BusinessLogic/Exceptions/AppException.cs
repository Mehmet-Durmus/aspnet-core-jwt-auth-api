using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; set; }
        protected AppException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
        protected AppException(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
