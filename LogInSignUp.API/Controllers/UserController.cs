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
            EmailRequestAvailabilityDto response = await _userManager.CreateUserAsync(createUserDto);
            return Created(String.Empty, response);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyTokenDto verifyTokenDto)
        {
            await _userManager.VerifyEmailAsync(verifyTokenDto);
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendVerificationMail(UserIdentifierDto userIdentifierDto)
        {
            EmailRequestAvailabilityDto response = await _userManager.SendNewVerificationEmailAsync(userIdentifierDto);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendResetPasswordMail(UserIdentifierDto userIdentifierDto) 
        {
            EmailRequestAvailabilityDto response = await _userManager.SendResetPasswordMailAsync(userIdentifierDto);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyResetPasswordToken(VerifyTokenDto verifyTokenDto)
        {
            bool result = await _userManager.VerifyResetPasswordTokenAsync(verifyTokenDto);
            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            await _userManager.UpdatePassword(updatePasswordDto);
            return NoContent();
        }
    }
}
