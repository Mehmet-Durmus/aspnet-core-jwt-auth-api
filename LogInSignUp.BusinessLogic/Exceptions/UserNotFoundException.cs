using System.Globalization;

namespace LogInSignUp.BusinessLogic.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException(int statusCode, string message)
            :base(statusCode, message) {}
        public UserNotFoundException(int statusCode)
            :base(statusCode, "The requested user could not be found.") {}
        public UserNotFoundException()
            : base(404, "The requested user could not be found.") { }
    }
}
