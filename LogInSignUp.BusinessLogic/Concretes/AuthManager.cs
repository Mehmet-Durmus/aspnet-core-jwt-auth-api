using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Exceptions;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Concretes
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthManager(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
        }

        public async Task<AccessTokenDto> LogInAsync(string userNameOrEmail, string password)
        {
            User user = await _userRepository.GetUserByUserName(userNameOrEmail);
            if (user == null)
                user = await _userRepository.GetUserByEmail(userNameOrEmail);
            if (user == null)
                throw new UserNotFoundException();
            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new InvalidCredentialsException();
            if (!user.IsEmailVerified)
                throw new EmailNotVerifiedException();
            return _tokenHandler.CreateAccessToken(user);
        }
    }
}
