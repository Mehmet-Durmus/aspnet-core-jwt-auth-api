using AutoMapper;
using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.Configuration.Mail;
using LogInSignUp.BusinessLogic.Configuration.Token;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Enums;
using LogInSignUp.BusinessLogic.Exceptions;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Entities;

namespace LogInSignUp.BusinessLogic.Concretes
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenSettings _tokenSettings;
        private readonly EmailVerificationTokenSettings _emailVerificationTokenSettings;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly ResetPasswordTokenSettings _resetPasswordTokenSettings;
        private readonly MailSettings _mailSettings;
        private readonly EmailVerificationMailSettings _emailVerificationMailSettings;
        private readonly ResetPasswordMailSettings _resetPasswordMailSettings;
        private readonly ITokenHandler _tokenHandler;
        private readonly ITokenHasher _tokenHasher;
        private readonly IMailService _mailService;

        public UserManager(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            TokenSettings tokenSettings,
            MailSettings mailSettings,
            ITokenHandler tokenHandler,
            ITokenHasher tokenHasher,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
            _emailVerificationTokenSettings = _tokenSettings.EmailVerificationSettings;
            _refreshTokenSettings = _tokenSettings.RefreshTokenSettings;
            _resetPasswordTokenSettings = _tokenSettings.ResetPasswordSettings;
            _mailSettings = mailSettings;
            _emailVerificationMailSettings = _mailSettings.EmailVerificationSettings;
            _resetPasswordMailSettings = _mailSettings.ResetPasswordSettings;
            _tokenHandler = tokenHandler;
            _tokenHasher = tokenHasher;
            _mailService = mailService;
        }

        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await _userRepository.EmailExistsAsync(createUserDto.Email))
                throw new EmailAlreadyExistsException(createUserDto.Email);
            if (await _userRepository.UserNameExistsAsync(createUserDto.UserName))
                throw new UserNameAlreadyExistsException(createUserDto.UserName);
            if (createUserDto.Password != createUserDto.PasswordConfirm)
                throw new PasswordMismatchException();

            User user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _passwordHasher.Hash(createUserDto.Password);
            user = await _userRepository.AddAsync(user);

            await IssueEmailVerificationAsync(user);
        }
        public async Task VerifyEmailAsync(string userId, string verificationToken)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            if (DateTime.UtcNow > user.EmailVerificationTokenEndDate)
                throw new EmailVerificationTokenExpiredException();
            if (!_tokenHasher.Verify(verificationToken, user.EmailVerificationTokenHash))
                throw new InvalidEmailVerificationTokenException();
            user.IsEmailVerified = true;
            user.EmailVerificationTokenHash = null;
            await _userRepository.UpdateAsync(user);
        }

        public async Task SendNewVerificationEmailAsync(string userNameOrEmail)
        {
            User? user = await _userRepository.GetUserByUserName(userNameOrEmail);
            if (user == null)
                user = await _userRepository.GetUserByEmail(userNameOrEmail);
            if (user == null)
                throw new UserNotFoundException();
            if (user.IsEmailVerified)
                throw new EmailAlreadyVerifiedException();
            if (user.EmailVerificationTokenSentAt > DateTime.UtcNow.AddMinutes(-_emailVerificationMailSettings.ResendCooldownMinutes))
                throw new EmailVerificationAlreadyRequestedException();    
            await IssueEmailVerificationAsync(user);
        }

        private async Task IssueEmailVerificationAsync(User user)
        {
            string emailVerificationToken = _tokenHandler.CreateToken(TokenEncoding.UrlSafe);
            await _mailService.SendEmailVerificationMailAsync(user, emailVerificationToken);
            user.EmailVerificationTokenHash = _tokenHasher.Hash(emailVerificationToken);
            user.EmailVerificationTokenEndDate = DateTime.UtcNow.AddMinutes(_emailVerificationTokenSettings.TokenLifetimeMinutes);
            user.EmailVerificationTokenSentAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateRefreshTokenAsync(User user, string refreshToken)
        {
            if (user != null)
            {
                user.RefreshTokenHash = _tokenHasher.Hash(refreshToken);
                user.RefreshTokenEndDate = DateTime.UtcNow.AddDays(_refreshTokenSettings.TokenLifetimeDays);
                await _userRepository.UpdateAsync(user);
            }
            else
                throw new UserNotFoundException();
        }

        public async Task SendResetPasswordMailAsync(string email)
        {
            User? user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new UserNotFoundException();
            if (user.PasswordResetTokenSentAt > DateTime.UtcNow.AddMinutes(-_resetPasswordMailSettings.ResendCooldownMinutes))
                throw new ResetPasswordAlreadyRequestedException();
            string resetPasswordToken = _tokenHandler.CreateToken(TokenEncoding.UrlSafe);
            await _mailService.SendResetPasswordMailAsync(user, resetPasswordToken);
            user.PasswordResetTokenHash = _tokenHasher.Hash(resetPasswordToken);
            user.PasswordResetTokenEndDate = DateTime.UtcNow.AddMinutes(_resetPasswordTokenSettings.TokenLifetimeMinutes);
            user.IsPasswordResetTokenUsed = false;
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> VerifyResetPasswordTokenAsync(string userId, string resetPasswordToken)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            bool result = _tokenHasher.Verify(resetPasswordToken, user.PasswordResetTokenHash);
            if (!result)
                throw new InvalidResetPasswordTokenException();
            if (DateTime.UtcNow > user.RefreshTokenEndDate)
                throw new ResetPasswordTokenExpiredException();
            return result;
        }

        public async Task UpdatePassword(string userId, string password, string passwordConfirm)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            if (password != passwordConfirm)
                throw new PasswordMismatchException();
            user.PasswordHash = _passwordHasher.Hash(password);
            await _userRepository.UpdateAsync(user);
        }
    }
}
