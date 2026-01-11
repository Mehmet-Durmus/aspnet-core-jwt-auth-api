using LogInSignUp.BusinessLogic.DTOs;
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
        string CreateToken();
        AccessTokenDto CreateAccessToken(User user);
    }
}
