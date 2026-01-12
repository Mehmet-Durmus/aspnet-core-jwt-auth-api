using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Enums;
using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Token.Abstracts
{
    public interface ITokenHandler
    {
        AccessTokenDto CreateAccessToken(User user);
        string CreateToken(TokenEncoding encoding);
    }
}
