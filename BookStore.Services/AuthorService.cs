using BookStore.Domain.Models;
using BookStore.Repositories.Generics;
using BookStore.Services.Generics;

namespace BookStore.Services
{
    public class AuthorService : GenericService<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}