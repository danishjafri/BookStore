using BookStore.Domain.Models;
using BookStore.Services.Generics;

namespace BookStore.Services
{
    public interface IBookService : IGenericService<Book> { }

}