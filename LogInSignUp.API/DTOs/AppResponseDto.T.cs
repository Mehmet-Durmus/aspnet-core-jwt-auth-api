using System.Security.Principal;

namespace LogInSignUp.API.DTOs
{
    public class AppResponseDto<T>
    {
        public T Data { get; set; }

        public static AppResponseDto<T> Success(T data)
            => new() { Data = data };
    }
}
