using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class RefreshTokenLogInValidator : AbstractValidator<RefreshTokenLogInDto>
    {
        public RefreshTokenLogInValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage("Invalid user id.");
            RuleFor(x => x.RefreshToken)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Invalid refresh token request.")
                .Length(44).WithMessage("Invalid refresh token request.")
                .Matches(@"^[A-Za-z0-9+/]+={0,2}$")
                .WithMessage("Invalid refresh token request.");
        }
    }
}
