using System.Collections.Generic;

namespace BookStore.API.DTOs.Authors
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }

        // Relationships
        public virtual IList<BookDto> Books { get; set; }
    }
}