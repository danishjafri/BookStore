using BookStore.Domain.Models;
using BookStore.Repositories.Generics;
using BookStore.Services.Generics;

namespace BookStore.Services
{
    public class BookService : GenericService<Book>, IBookService
    {
        public BookService(IUnitOfWork uow) : base(uow) { }
    }
}