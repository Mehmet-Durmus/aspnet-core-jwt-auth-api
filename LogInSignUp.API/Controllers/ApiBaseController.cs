using LogInSignUp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult CreateActionResult<T>(AppResponseDto<T> response)
            => StatusCode(response.StatusCode, response);
        protected IActionResult CreateActionResult(AppResponseDto response)
            => StatusCode(response.StatusCode, response);
    }
}
