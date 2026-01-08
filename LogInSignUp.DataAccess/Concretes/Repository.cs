using LogInSignUp.DataAccess.Abstracts;
using LogInSignUp.DataAccess.Context;
using LogInSignUp.DataAccess.Entities;
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

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsActive = false;
                return await UpdateAsync(entity);
            }
            return false;
        }

        public IQueryable<T> GetAll()
            => _dbSet;

        public async Task<T> GetAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<bool> UpdateAsync(T entity)
        {
            EntityEntry<T> entityEntry = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Modified;
        }
    }
}
