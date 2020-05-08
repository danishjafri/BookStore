using BookStore.Domain;
using BookStore.Repositories;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class GenericServices<T> where T : BaseEntity, IGenericServices<T>
    {
        private readonly IUnitOfWork _uow;

        public GenericServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<T> GetById(int id)
        {
            return _uow.GetRepository<T>().SingleAsync(a => a.Id == id);
        }

        public IPaginate<T> GetListWithPagination() => _uow.GetRepository<T>().GetList();

        public async Task<T> Update(T entity)
        {
            await _uow.GetRepository<T>().UpdateAsync(entity);
            await _uow.CommitAsync();
            return entity;

        }
    }
}
