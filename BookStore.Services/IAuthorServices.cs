using BookStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthorServices
    {
        Task<IEnumerable<Author>> GetListOfAuthorsAsync();
        Task<Author> UpdateAuthor(Author author);
    }
}