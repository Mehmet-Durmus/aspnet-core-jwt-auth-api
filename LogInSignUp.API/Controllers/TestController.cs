using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.UserManagement.Absracts;
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

        public TestController(IUserManager userManager, IPasswordHasher hash)
        {
            _userManager = userManager;
            _hash = hash;
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
    }
}
