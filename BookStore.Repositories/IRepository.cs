using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int Id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);
    }
}
