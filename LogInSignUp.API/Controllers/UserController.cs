using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            await _userManager.CreateUserAsync(createUserDto);
            return Ok();
        }

        [HttpGet("verify-email/{userId}/{verificationToken}")]
        public async Task<IActionResult> VerifyEmail(string userId, string verificationToken)
        {
            await _userManager.VerifyEmail(userId, verificationToken);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendVerificationMail(string userId)
        {
            await _userManager.SendNewVerificationEmail(userId);
            return Ok();
        }
    }
}
