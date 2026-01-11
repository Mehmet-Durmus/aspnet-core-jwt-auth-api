using LogInSignUp.BusinessLogic.Configuration.Token;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using LogInSignUp.DataAccess.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Token.Concretes
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenSettings _tokenSettings;
        private readonly JwtSettings _jwtSettings;

        public TokenHandler(TokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
            _jwtSettings = _tokenSettings.JwtSettings;
        }

        public AccessTokenDto CreateAccessToken(User user)
        {
            AccessTokenDto token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeDays);
            JwtSecurityToken securityToken = new(
                audience: _jwtSettings.Audience,
                issuer: _jwtSettings.Issuer,
                expires: token.Expiration,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserName", user.UserName)
                },
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public string CreateToken()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(32);
            return WebEncoders.Base64UrlEncode(bytes);
        }


    }
}
