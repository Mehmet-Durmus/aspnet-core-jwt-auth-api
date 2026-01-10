using LogInSignUp.DataAccess.Entities;
using LogInSignUp.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Abstracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Guid id);
        IQueryable<T> GetAll();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<DeleteResult> DeleteAsync(Guid id);
    }
}
