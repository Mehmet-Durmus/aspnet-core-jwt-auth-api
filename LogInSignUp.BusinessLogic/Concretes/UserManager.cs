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

        public async Task<EmailRequestAvailabilityDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await _userRepository.EmailExistsAsync(createUserDto.Email))
                throw new EmailAlreadyExistsException(createUserDto.Email);
            if (await _userRepository.UserNameExistsAsync(createUserDto.UserName))
                throw new UserNameAlreadyExistsException(createUserDto.UserName);

            User user = _mapper.Map<User>(createUserDto);
            user.Name = user.Name.Trim();
            user.LastName = user.LastName.Trim();
            user.UserName = user.UserName.Trim();
            user.PasswordHash = _passwordHasher.Hash(createUserDto.Password);
            user = await _userRepository.AddAsync(user);

            await IssueEmailVerificationAsync(user);

            return new() { EmailRequestCooldownInSeconds = _emailVerificationMailSettings.ResendCooldownSeconds };

        }
        public async Task VerifyEmailAsync(VerifyTokenDto verifyTokenDto)
        {
            byte[] computedTokenHash = _tokenHasher.Hash(verifyTokenDto.Token);
            User? user = await _userRepository.GetUserByEmailVerificationToken(computedTokenHash);
            if (user == null)
                throw new InvalidEmailVerificationTokenException();
            if (DateTime.UtcNow > user.EmailVerificationTokenEndDate)
                throw new EmailVerificationTokenExpiredException();
            user.IsEmailVerified = true;
            user.EmailVerificationTokenHash = null;
            await _userRepository.UpdateAsync(user);
        }

        public async Task<EmailRequestAvailabilityDto> SendNewVerificationEmailAsync(UserIdentifierDto userIdentifierDto)
        {
            User? user = await _userRepository.GetUserByUserNameOrEmail(userIdentifierDto.UserNameOrEmail);
            if (user == null)
                throw new UserNotFoundException();
            if (user.IsEmailVerified)
                throw new EmailAlreadyVerifiedException();
            if (user.EmailVerificationTokenSentAt > DateTime.UtcNow.AddSeconds(-_emailVerificationMailSettings.ResendCooldownSeconds))
                throw new EmailVerificationAlreadyRequestedException();    
            await IssueEmailVerificationAsync(user);

            return new() { EmailRequestCooldownInSeconds = _emailVerificationMailSettings.ResendCooldownSeconds };
        }

        private async Task IssueEmailVerificationAsync(User user)
        {
            string emailVerificationToken = _tokenHandler.CreateToken(TokenEncoding.UrlSafe);
            await _mailService.SendEmailVerificationMailAsync(user.Email, emailVerificationToken);
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

        public async Task<EmailRequestAvailabilityDto> SendResetPasswordMailAsync(UserIdentifierDto userIdentifierDto)
        {
            User? user = await _userRepository.GetUserByUserNameOrEmail(userIdentifierDto.UserNameOrEmail);
            if (user == null)
                throw new UserNotFoundException();
            if (user.PasswordResetTokenSentAt.HasValue && 
                user.PasswordResetTokenSentAt > DateTime.UtcNow.AddSeconds(-_resetPasswordMailSettings.ResendCooldownSeconds))
                throw new ResetPasswordAlreadyRequestedException();
            string resetPasswordToken = _tokenHandler.CreateToken(TokenEncoding.UrlSafe);
            await _mailService.SendResetPasswordMailAsync(user.Email, resetPasswordToken);
            user.PasswordResetTokenHash = _tokenHasher.Hash(resetPasswordToken);
            user.PasswordResetTokenEndDate = DateTime.UtcNow.AddMinutes(_resetPasswordTokenSettings.TokenLifetimeMinutes);
            user.PasswordResetTokenSentAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return new() { EmailRequestCooldownInSeconds = _resetPasswordMailSettings.ResendCooldownSeconds };
        }

        public async Task<bool> VerifyResetPasswordTokenAsync(VerifyTokenDto verifyTokenDto)
        {
            byte[] computedTokenHash = _tokenHasher.Hash(verifyTokenDto.Token);
            User? user = await _userRepository.GetUserByResetPasswordToken(computedTokenHash);
            if (user == null)
                throw new InvalidResetPasswordTokenException();
            if (DateTime.UtcNow > user.RefreshTokenEndDate)
                throw new ResetPasswordTokenExpiredException();
            return true;
        }

        public async Task UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            byte[] computedTokenHash = _tokenHasher.Hash(updatePasswordDto.Token);
            User? user = await _userRepository.GetUserByResetPasswordToken(computedTokenHash);
            if (user == null)
                throw new InvalidResetPasswordTokenException();
            user.PasswordHash = _passwordHasher.Hash(updatePasswordDto.Password);
            user.PasswordResetTokenHash = null;
            await _userRepository.UpdateAsync(user);
        }
    }
}
