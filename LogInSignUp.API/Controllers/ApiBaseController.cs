using LogInSignUp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogInSignUp.API.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult CreateActionResult<T>(int statusCode, AppResponseDto<T> response)
            => StatusCode(statusCode, response);
        [NonAction]
        protected IActionResult CreateActionResult(int statusCode, AppResponseDto response)
            => StatusCode(statusCode, response);
        [NonAction]
        protected IActionResult NoContentResult()
            => NoContent();
    }
}
