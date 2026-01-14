namespace LogInSignUp.API.DTOs
{
    public class AppResponseDto
    {
        public string[]? Errors { get; set; }

        public static AppResponseDto Fail(string[] errors)
            => new() {Errors = errors };
        public static AppResponseDto Fail(string error)
            => new() {Errors = new[] { error } };
    }
}
