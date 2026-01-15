using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Abstracts
{
    public interface IUserManager
    {
        Task<EmailRequestAvailabilityDto> CreateUserAsync(CreateUserDto createUserDto);
        Task VerifyEmailAsync(VerifyTokenDto verifyTokenDto);
        Task<EmailRequestAvailabilityDto> SendNewVerificationEmailAsync(UserIdentifierDto userIdentifierDto);
        Task UpdateRefreshTokenAsync(User user, string refreshToken);
        Task<EmailRequestAvailabilityDto> SendResetPasswordMailAsync(UserIdentifierDto userIdentifierDto);
        Task<bool> VerifyResetPasswordTokenAsync(VerifyTokenDto verifyTokenDto);
        Task UpdatePassword(UpdatePasswordDto updatePasswordDto);
    }
}
