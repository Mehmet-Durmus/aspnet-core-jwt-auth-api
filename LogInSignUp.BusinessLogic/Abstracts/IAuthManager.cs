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
        Task<AccessTokenDto> LogInAsync(LogInDto logInDto);
        Task<AccessTokenDto> RefreshTokenLogInAsyn(RefreshTokenLogInDto refreshTokenLogInDto);
    }
}
