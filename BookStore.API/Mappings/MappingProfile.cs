using AutoMapper;
using BookStore.API.DTOs;
using BookStore.API.DTOs.Authors;
using BookStore.Domain.Models;

namespace BookStore.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorCreationDto>();
            CreateMap<Author, AuthorUpdateDto>();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}