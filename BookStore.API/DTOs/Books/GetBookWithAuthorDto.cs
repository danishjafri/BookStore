using BookStore.API.DTOs.Authors;

namespace BookStore.API.DTOs.Books
{
    public class GetBookWithAuthorDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }
        public GetAuthorWithoutBooks Author { get; set; }
    }
}