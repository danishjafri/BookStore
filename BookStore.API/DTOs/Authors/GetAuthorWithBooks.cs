using BookStore.API.DTOs.Books;
using System.Collections.Generic;

namespace BookStore.API.DTOs.Authors
{
    public class GetAuthorWithBooks
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }
        public virtual IList<GetBookWithoutAuthorDto> Books { get; set; }
    }
}