using System.Collections.Generic;

namespace BookStore.Domain.Models
{
    public class Author : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }

        // Relationships
        public virtual IList<Book> Books { get; set; }
    }
}