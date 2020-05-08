using BookStore.Domain;
using BookStore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.EFRepositories
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new GenericRepository<TEntity>(Context);
            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public TContext Context { get; }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();

        }
    }
}
