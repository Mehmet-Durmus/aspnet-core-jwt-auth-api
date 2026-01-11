namespace LogInSignUp.API.DTOs
{
    public class AppResponseDto
    {
        public int StatusCode { get; set; }
        public string[]? Errors { get; set; }

        public static AppResponseDto Success(int statusCode)
            => new() { StatusCode = statusCode };
        public static AppResponseDto Fail(int statusCode, string[] errors)
            => new() { StatusCode = statusCode, Errors = errors };
        public static AppResponseDto Fail(int statusCode, string error)
            => new() { StatusCode = statusCode, Errors = new[] { error } };
    }
}
