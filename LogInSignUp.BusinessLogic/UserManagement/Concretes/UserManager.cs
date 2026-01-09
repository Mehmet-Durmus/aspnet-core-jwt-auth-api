using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Exceptions;
using LogInSignUp.BusinessLogic.UserManagement.Absracts;
using LogInSignUp.DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogInSignUp.DataAccess.Entities;
using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using Microsoft.Extensions.Options;
using LogInSignUp.BusinessLogic.Configuration;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;

namespace LogInSignUp.BusinessLogic.UserManagement.Concretes
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenSettings _tokenSettings;
        private readonly ITokenHandler _tokenHabdler;
        private readonly ITokenHasher _tokenHasher;

        public UserManager(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            TokenSettings tokenSettings,
            ITokenHandler tokenHabdler,
            ITokenHasher tokenHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
            _tokenHabdler = tokenHabdler;
            _tokenHasher = tokenHasher;
        }

        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await _userRepository.EmailExistsAsync(createUserDto.Email))
                throw new EmailAlreadyExistsException(createUserDto.Email);
            if (await _userRepository.UserNameExistsAsync(createUserDto.UserName))
                throw new UserNameAlreadyExistsException(createUserDto.UserName);
            if (createUserDto.Password != createUserDto.PasswordConfirm)
                throw new PasswordMismatchException();
            User user = new()
            {
                Name = createUserDto.Name,
                LastName = createUserDto.LastName,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
            };
            string emailVerificationToken = _tokenHabdler.CreateEmailVerificationToken();
            // _mailManager.SendEmailVerificationMail(...);
            user.EmailVerificationTokenHash = _tokenHasher.Hash(emailVerificationToken);
            user.EmailVerificationTokenEndDate = DateTime.UtcNow.AddMinutes(_tokenSettings.EmailVerificationTokenLifeTimeInMinutes);
            user.PasswordHash = _passwordHasher.Hash(createUserDto.Password);
            await _userRepository.AddAsync(user);
        }
    }
}
