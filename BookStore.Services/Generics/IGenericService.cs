using BookStore.Domain;
using BookStore.Repositories.Generics;
using System.Threading.Tasks;

namespace BookStore.Services.Generics
{
    public interface IGenericService<T> where T : BaseEntity
    {
        IPaginate<T> GetListWithPagination();

        Task<T> GetById(int id);

        Task<T> Update(T entity);

        Task<int> CreateAsync(T entity);

        Task Delete(int id);
    }
}