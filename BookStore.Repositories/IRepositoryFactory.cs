using BookStore.Domain;

namespace BookStore.Repositories
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
