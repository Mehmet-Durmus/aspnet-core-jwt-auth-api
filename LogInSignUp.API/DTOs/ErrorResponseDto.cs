namespace LogInSignUp.API.DTOs
{
    public class ErrorResponseDto
    {
        public string[] Errors { get; set; }

        public ErrorResponseDto(string[] errors)
        {
            Errors = errors;
        }

        public ErrorResponseDto(string error)
        {
            Errors = new[] { error };
        }
    }
}
