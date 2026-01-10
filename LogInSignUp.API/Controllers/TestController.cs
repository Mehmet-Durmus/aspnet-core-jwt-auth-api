using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly IUserManager _userManager;
        readonly IPasswordHasher _hash;
        readonly IMailService _mail;
        readonly ITokenHandler _token;

        public TestController(IUserManager userManager, IPasswordHasher hash, IMailService mail, ITokenHandler token)
        {
            _userManager = userManager;
            _hash = hash;
            _mail = mail;
            _token = token;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignUpAsync(CreateUserDto createUserDto)
        {
            await _userManager.CreateUserAsync(createUserDto);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult TestPasswordHasher(string password)
        {
            string hash = _hash.Hash(password);
            bool result = _hash.Verify(password, hash);
            return Ok($"" +
                $"Hash: {hash}\n" +
                $"Result: {result}"
            );
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendVerificationMail(string to, string userId)
        {
            //string tokenV = _token.CreateEmailVerificationToken();
            //await _mail.SendEmailVerificationMail(to, userId, "qwerty");
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken()
        {
            string token = _token.CreateToken();
            return Ok(token);
        }
    }
}
