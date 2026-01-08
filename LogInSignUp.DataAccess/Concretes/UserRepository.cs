using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Context;
using LogInSignUp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Concretes
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstAsync(u => u.Email== email);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _dbSet.FirstAsync(u => u.UserName == userName);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _dbSet.AnyAsync(x => x.UserName == userName);
        }
    }
}
