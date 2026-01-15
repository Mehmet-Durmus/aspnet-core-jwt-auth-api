using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class LogInValidator : AbstractValidator<LogInDto>
    {
        public LogInValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Provide user name or email.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Provide a password.");
        }
    }
}
