using BookStore.Domain;
using BookStore.Repositories.Generics;
using System.Threading.Tasks;

namespace BookStore.Services.Generics
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _uow;

        public GenericService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateAsync(T entity)
        {
            await _uow.GetRepository<T>().AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task Delete(int id)
        {
            _uow.GetRepository<T>().Delete(id);
            await _uow.CommitAsync();
        }

        public Task<T> GetById(int id)
        {
            return _uow.GetRepository<T>().SingleAsync(a => a.Id == id);
        }

        public IPaginate<T> GetListWithPagination() => _uow.GetRepository<T>().GetList();

        public async Task<T> Update(T entity)
        {
            _uow.GetRepository<T>().Update(entity);
            await _uow.CommitAsync();
            return entity;
        }
    }
}