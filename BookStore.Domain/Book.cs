﻿namespace BookStore.Domain.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }

        // Relationships
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}