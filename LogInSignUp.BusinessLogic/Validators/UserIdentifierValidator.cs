using FluentValidation;
using LogInSignUp.BusinessLogic.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Validators
{
    public class UserIdentifierValidator : AbstractValidator<UserIdentifierDto>
    {
        public UserIdentifierValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Provide email or user name.");
                
        }
    }
}
