namespace LogInSignUp.BusinessLogic.DTOs
{
    public class LogInDto
    {
        public required string UserNameOrEmail { get; set; }
        public required string Password { get; set; } = null!;
    }
}
