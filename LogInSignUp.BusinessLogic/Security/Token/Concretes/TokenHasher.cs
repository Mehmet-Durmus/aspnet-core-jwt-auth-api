using LogInSignUp.BusinessLogic.Security.Token.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Token.Concretes
{
    public class TokenHasher : ITokenHasher
    {
        public byte[] Hash(string token)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
        }

        public bool Verify(string token, byte[] storedHash)
        {
            byte[] computedHash = Hash(token);

            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
