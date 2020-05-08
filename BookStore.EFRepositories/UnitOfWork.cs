using BookStore.Domain.Data;
using BookStore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookStore.EFRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }


        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();

        }
    }
}
