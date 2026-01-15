using LogInSignUp.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LogInSignUp.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                throw new ValidationException(errors);
            }
            await next();
        }
    }
}
