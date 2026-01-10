using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsEmailVerified { get; set; }
        public byte[]? EmailVerificationTokenHash { get; set; }
        public DateTime? EmailVerificationTokenEndDate { get; set; }
        public string PasswordHash { get; set; } = null!;
        public byte[]? PasswordResetTokenHash { get; set; }
        public DateTime? PasswordResetTokenEndDate { get; set; }
        public bool IsPasswordResetTokenUsed { get; set; }
        public byte[]? RefreshTokenHash { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
