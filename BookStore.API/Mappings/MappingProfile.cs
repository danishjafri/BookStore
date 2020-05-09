using AutoMapper;
using BookStore.API.DTOs.Authors;
using BookStore.API.DTOs.Books;
using BookStore.Domain.Models;

namespace BookStore.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Authors
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorCreationDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, GetAuthorWithBooks>();
            CreateMap<Author, GetAuthorWithoutBooks>();

            // Buthors
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookCreationDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, GetBookWithAuthorDto>();
            CreateMap<Book, GetBookWithoutAuthorDto>();
        }
    }
}