using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        Task CommitAsync();
    }
}
