using BookStore.Domain;
using BookStore.Repositories;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IGenericServices<T> where T : BaseEntity
    {
        IPaginate<T> GetListWithPagination();
        Task<T> GetById(int id);
        Task<T> Update(T entity);
    }
}