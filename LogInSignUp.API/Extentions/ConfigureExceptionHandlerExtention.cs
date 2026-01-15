using LogInSignUp.API.DTOs;
using LogInSignUp.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace LogInSignUp.API.Extentions
{
    public static class ConfigureExceptionHandlerExtention
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(builder => 
            {
                builder.Run(async context => 
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature?.Error is ValidationException validationException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new ErrorResponseDto(validationException.Errors));
                        return;
                    }
                    else if(contextFeature?.Error is AppException appException)
                    {
                        context.Response.StatusCode = appException.StatusCode;
                        await context.Response.WriteAsJsonAsync(new ErrorResponseDto(appException.Message));
                        return;
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var unknownResponse = new ErrorResponseDto(contextFeature.Error.Message);

                    await context.Response.WriteAsJsonAsync(unknownResponse);
                });
            });
        }
    }
}
