using LogInSignUp.API.DTOs;
using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn(string userNameOrEmail, string password)
        {
            AccessTokenDto token = await _authManager.LogInAsync(userNameOrEmail, password);
            return CreateActionResult(AppResponseDto<AccessTokenDto>.Success(200, token));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogIn(string userId, string refreshToken)
        {
            AccessTokenDto token = await _authManager.RefreshTokenLogInAsyn(userId, refreshToken);
            return CreateActionResult(AppResponseDto<AccessTokenDto>.Success(200, token));
        }
    }
}
