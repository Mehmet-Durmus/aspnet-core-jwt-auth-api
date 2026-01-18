using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class VerifyTokenValidator : AbstractValidator<VerifyTokenDto>
    {
        public VerifyTokenValidator()
        {
            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Invalid token.")
                .Length(40, 60).WithMessage("Invalid token.")
                .Matches("^[A-Za-z0-9_-]+$").WithMessage("Invalid token.");
        }
    }
}
