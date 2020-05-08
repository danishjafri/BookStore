using System.Collections.Generic;

namespace BookStore.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }

        // Relationships
        public virtual IList<Book> Books { get; set; }
    }
}
