using BookStore.Domain.Models;
using BookStore.Repositories;

namespace BookStore.Services
{
    public class AuthorService : GenericService<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork uow) : base(uow) { }
    }
}
