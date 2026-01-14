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

                    if(contextFeature?.Error is AppException appException)
                    {
                        context.Response.StatusCode = appException.StatusCode;
                        var response = AppResponseDto.Fail(appException.Message);

                        await context.Response.WriteAsJsonAsync(response);
                        return;
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var unknownResponse = AppResponseDto.Fail("Unexpected Error!");

                    await context.Response.WriteAsJsonAsync(unknownResponse);
                });
            });
        }
    }
}
