using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Books
{
    public class BookCreationDto
    {
        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }

        [Required]
        public string ISBN { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}