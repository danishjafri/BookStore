namespace BookStore.API.DTOs.Authors
{
    public class AuthorUpdateDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }
    }
}