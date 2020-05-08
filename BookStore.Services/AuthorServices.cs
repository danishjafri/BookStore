using BookStore.Domain.Models;
using BookStore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Author> _repository;

        public AuthorServices(IUnitOfWork uow, IRepository<Author> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public async Task<IEnumerable<Author>> GetListOfAuthorsAsync() => await _repository.GetAsync();

        public async Task<Author> UpdateAuthor(Author author)
        {
            _repository.Update(author);
            await _uow.CommitAsync();
            return author;

        }
    }
}
