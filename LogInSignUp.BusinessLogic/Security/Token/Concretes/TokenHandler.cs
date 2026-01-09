using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Token.Concretes
{
    public class TokenHandler : ITokenHandler
    {
        public string CreateEmailVerificationToken()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(32);
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
