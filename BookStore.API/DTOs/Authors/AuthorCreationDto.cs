using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.Authors
{
    public class AuthorCreationDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Bio { get; set; }
    }
}