using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Security.Token.Abstracts
{
    public interface ITokenHasher
    {
        byte[] Hash(string token);
        bool Verify(string token, byte[] storedHash);
    }
}
