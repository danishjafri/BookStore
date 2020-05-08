using AutoMapper;
using BookStore.API.DTOs;
using BookStore.Domain.Models;

namespace BookStore.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
