using BookStore.API.DTOs.Authors;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Books
{
    public class BookUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }
        public int? AuthorId { get; set; }
    }
}