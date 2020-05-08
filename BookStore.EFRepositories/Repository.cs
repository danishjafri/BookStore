using BookStore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.EFRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _unitOfWork.Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            T existing = await _unitOfWork.Context.Set<T>().FindAsync(entity);
            if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _unitOfWork.Context.Set<T>().Where(predicate).ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _unitOfWork.Context.Set<T>().FindAsync(Id);
        }

        public void Update(T entity)
        {
            _unitOfWork.Context.Entry<T>(entity).State = EntityState.Modified;
            _unitOfWork.Context.Set<T>().Attach(entity);
        }
    }
}
