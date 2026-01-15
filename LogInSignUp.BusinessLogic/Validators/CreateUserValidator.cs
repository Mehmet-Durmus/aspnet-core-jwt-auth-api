using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Name)
                .Must(x => x.Trim().Length >= 2).WithMessage("Name must be at least 2 non-whitespace characters.")
                .MaximumLength(50).WithMessage("Name can be a maximum of 50 characters.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Name can't contain numbers.");
            RuleFor(u => u.LastName)
                .Must(x => x.Trim().Length >= 2).WithMessage("Last name must be at least 2 non-whitespace characters.")
                .MaximumLength(50).WithMessage("Last name can be a maximum of 50 characters.")
                .Matches(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ\s]+$").WithMessage("Last name can't contain numbers.");
            RuleFor(u => u.UserName)
                .Must(x => x.Trim().Length >= 2).WithMessage("User name must be at least 2 non-whitespace characters.")
                .MaximumLength(50).WithMessage("User name can be a maximum of 50 characters.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Provide a valid email address.");
            RuleFor(u => u.Password)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(50).WithMessage("Password can be a maximum of 50 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[^\w\s]").WithMessage("Password must contain at least one special character.");
            RuleFor(u => u.PasswordConfirm).Equal(u => u.Password).WithMessage("Passwords do not match.");
        }
    }
}
