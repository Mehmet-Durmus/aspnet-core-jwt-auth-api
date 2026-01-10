using LogInSignUp.BusinessLogic.DTOs;
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
        Task VerifyEmail(string userId, string verificationToken);
    }
}
