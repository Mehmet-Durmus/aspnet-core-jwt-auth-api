using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage("Invalid user id.");
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
