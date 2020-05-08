using BookStore.Domain.Models;
using BookStore.Repositories;

namespace BookStore.EFRepositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
