using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Exceptions;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Entities;

namespace LogInSignUp.BusinessLogic.Concretes
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;
        private readonly ITokenHasher _tokenHasher;
        private readonly IUserManager _userManager;

        public AuthManager(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenHandler tokenHandler,
            IUserManager userManager,
            ITokenHasher tokenHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _tokenHasher = tokenHasher;
        }

        public async Task<AccessTokenDto> LogInAsync(string userNameOrEmail, string password)
        {
            User? user = await _userRepository.GetUserByUserName(userNameOrEmail);
            if (user == null)
                user = await _userRepository.GetUserByEmail(userNameOrEmail);
            if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
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
                throw new UserNotFoundException(401);
            if (!_tokenHasher.Verify(refreshToken, user.RefreshTokenHash))
                throw new InvalidRefreshTokenException();
            if (DateTime.UtcNow > user.RefreshTokenEndDate)
                throw new RefreshTokenExpiredException();
            AccessTokenDto token = _tokenHandler.CreateAccessToken(user);
            await _userManager.UpdateRefreshTokenAsync(user, refreshToken);
            return token;
        }
    }
}
