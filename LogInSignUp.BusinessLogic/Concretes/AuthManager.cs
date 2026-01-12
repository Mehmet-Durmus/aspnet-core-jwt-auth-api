using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Enums;
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
        private readonly IUserManager _userManager;

        public AuthManager(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenHandler tokenHandler,
            IUserManager userManager)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
            _userManager = userManager;
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
            AccessTokenDto token = _tokenHandler.CreateAccessToken(user);
            await _userManager.UpdateRefreshTokenAsync(user, token.RefreshToken);
            return token;
        }

        public async Task<AccessTokenDto> RefreshTokenLogInAsyn(string userId, string refreshToken)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            if (user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                AccessTokenDto token = _tokenHandler.CreateAccessToken(user);
                await _userManager.UpdateRefreshTokenAsync(user, refreshToken);
                return token;
            }
            throw new TokenExpiredException();
        }
    }
}
