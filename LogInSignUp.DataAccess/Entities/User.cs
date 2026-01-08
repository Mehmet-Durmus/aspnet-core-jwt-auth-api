using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationToken { get; set; }
        public DateTime EmailVerificationTokenEndDate { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime PasswordResetTokenEndDate { get; set; }
        public bool IsPasswordResetTokenUsed { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
    }
}
