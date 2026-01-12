using LogInSignUp.API.DTOs;
using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
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
            await _userManager.VerifyEmailAsync(userId, verificationToken);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendVerificationMail(string userId)
        {
            await _userManager.SendNewVerificationEmailAsync(userId);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendResetPasswordMail(string userId)
        {
            await _userManager.SendResetPasswordMailAsync(userId);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> VerifyResetPasswordToken(string userId, string resetPasswordToken)
        {
            bool result = await _userManager.VerifyResetPasswordTokenAsync(userId, resetPasswordToken);
            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePassword(string userId, string password, string passwordConfirm)
        {
            await _userManager.UpdatePassword(userId, password, passwordConfirm);
            return Ok();
        }
    }
}
