using AutoMapper;
using LogInSignUp.BusinessLogic.Abstracts;
using LogInSignUp.BusinessLogic.Configuration.Token;
using LogInSignUp.BusinessLogic.DTOs;
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
        private readonly EmailVerificationSettings _emailVerificationSettings;
        private readonly ITokenHandler _tokenHandler;
        private readonly ITokenHasher _tokenHasher;
        private readonly IMailService _mailService;

        public UserManager(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            TokenSettings tokenSettings,
            ITokenHandler tokenHandler,
            ITokenHasher tokenHasher,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
            _emailVerificationSettings = _tokenSettings.EmailVerificationSettings;
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
        public async Task VerifyEmail(string userId, string verificationToken)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            if (DateTime.UtcNow > user.EmailVerificationTokenEndDate)
                throw new TokenExpiredException();
            if (!_tokenHasher.Verify(verificationToken, user.EmailVerificationTokenHash))
                throw new InvalidTokenException("Email verification token is invalid.");
            user.IsEmailVerified = true;
            user.EmailVerificationTokenHash = null;
            await _userRepository.UpdateAsync(user);
        }

        public async Task SendNewVerificationEmail(string userId)
        {
            User? user = await _userRepository.GetAsync(Guid.Parse(userId));
            if (user == null)
                throw new UserNotFoundException();
            if (user.IsEmailVerified)
                throw new EmailAlreadyVerifiedException();
            if (DateTime.UtcNow.AddSeconds(-_emailVerificationSettings.ResendCooldownSeconds) > user.EmailVerificationTokenSentAt)
                await IssueEmailVerificationAsync(user);
        }

        private async Task IssueEmailVerificationAsync(User user)
        {
            string emailVerificationToken = _tokenHandler.CreateToken();
            await _mailService.SendEmailVerificationMail(user, emailVerificationToken);
            user.EmailVerificationTokenHash = _tokenHasher.Hash(emailVerificationToken);
            user.EmailVerificationTokenEndDate = DateTime.UtcNow.AddMinutes(_emailVerificationSettings.TokenLifetimeMinutes);
            user.EmailVerificationTokenSentAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
        }
    }
}
