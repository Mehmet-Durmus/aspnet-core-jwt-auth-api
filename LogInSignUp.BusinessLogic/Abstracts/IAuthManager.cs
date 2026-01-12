using LogInSignUp.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Abstracts
{
    public interface IAuthManager
    {
        Task<AccessTokenDto> LogInAsync(string userNameOrEmail, string password);
        Task<AccessTokenDto> RefreshTokenLogInAsyn(string userId, string refreshToken);
    }
}
