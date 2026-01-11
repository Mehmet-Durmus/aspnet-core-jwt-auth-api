using System.Security.Principal;

namespace LogInSignUp.API.DTOs
{
    public class AppResponseDto<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        public static AppResponseDto<T> Success(int statusCode, T data)
            => new() { StatusCode = statusCode, Data = data };
    }
}
