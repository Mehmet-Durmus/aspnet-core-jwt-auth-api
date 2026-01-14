namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException()
            :base(401, "Invalid username, email or password.") {}
    }
}
