using LogInSignUp.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.UserManagement.Absracts
{
    public interface IUserManager
    {
        Task CreateUserAsync(CreateUserDto createUserDto);
    }
}
