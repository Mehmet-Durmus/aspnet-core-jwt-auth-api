using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Context;
using LogInSignUp.DataAccess.Entities;
using LogInSignUp.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<DeleteResult> DeleteAsync(Guid id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return DeleteResult.NotFound;
            else if (!entity.IsActive)
                return DeleteResult.AlreadyDeleted;
            else
            {
                entity.IsActive = false;
                await UpdateAsync(entity);
                return DeleteResult.Deleted;
            }
        }

        public IQueryable<T> GetAll()
            => _dbSet;

        public async Task<T?> GetAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
