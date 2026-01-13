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
        Task CreateUserAsync(CreateUserDto createUserDto);
        Task VerifyEmailAsync(string userId, string verificationToken);
        Task SendNewVerificationEmailAsync(string userId);
        Task UpdateRefreshTokenAsync(User user, string refreshToken);
        Task SendResetPasswordMailAsync(string email);
        Task<bool> VerifyResetPasswordTokenAsync(string userId, string resetPasswordToken);
        Task UpdatePassword(string userId, string password, string passwordConfirm);
    }
}
