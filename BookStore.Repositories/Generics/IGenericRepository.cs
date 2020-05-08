using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Repositories.Generics
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        Task<T> SearchAsync(params object[] keyValues);

        IPaginate<T> GetList(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true
        );

        IPaginate<TResult> GetList<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true
            ) where TResult : class;

        Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true
        );

        Task AddAsync(T entity);

        Task AddAsync(params T[] entities);

        Task AddAsync(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(object id);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        void Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);
    }
}