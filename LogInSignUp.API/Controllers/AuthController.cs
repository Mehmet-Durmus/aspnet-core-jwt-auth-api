using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            AccessTokenDto token = await _authManager.LogInAsync(logInDto);
            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogIn(RefreshTokenLogInDto refreshTokenLogInDto)
        {
            AccessTokenDto token = await _authManager.RefreshTokenLogInAsyn(refreshTokenLogInDto);
            return Ok(token);
        }
    }
}
