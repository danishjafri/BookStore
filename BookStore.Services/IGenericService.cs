using BookStore.Domain;
using BookStore.Repositories;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {
        IPaginate<T> GetListWithPagination();
        Task<T> GetById(int id);
        Task<T> Update(T entity);
        Task CreateAsync(T entity);
        Task Delete(int id);
    }
}