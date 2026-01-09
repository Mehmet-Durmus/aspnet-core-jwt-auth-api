using LogInSignUp.BusinessLogic.Security.Password.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Password.Concretes
{
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pbkdf2 = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    iterations: 100000,
                    HashAlgorithmName.SHA256
                );

            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }

        public bool Verify(string password, string storedHash)
        {
            var bytes = Convert.FromBase64String(storedHash);

            byte[] salt = bytes[..16];
            byte[] hash = bytes[16..];

            var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256);

            byte[] computed = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, computed);
        }
    }
}
