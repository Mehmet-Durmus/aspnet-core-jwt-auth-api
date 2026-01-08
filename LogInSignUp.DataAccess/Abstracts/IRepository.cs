using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Abstracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id);
        IQueryable<T> GetAll();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
