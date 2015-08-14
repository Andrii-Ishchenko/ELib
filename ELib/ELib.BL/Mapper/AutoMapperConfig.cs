using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System.Linq;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            configureGenreMapping();
        }

        private static void configureGenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Genre, GenreDto>();
            AutoMapper.Mapper.CreateMap<GenreDto, Genre>();

            AutoMapper.Mapper.CreateMap<Publisher, PublisherDto>();
            AutoMapper.Mapper.CreateMap<PublisherDto, Publisher>();


            AutoMapper.Mapper.CreateMap<Author, AuthorDto>();
            AutoMapper.Mapper.CreateMap<AuthorDto, Author>();

            AutoMapper.Mapper.CreateMap<Book, BookDto>()
               .ForMember(d => d.Authors, o => o.MapFrom(s => s.BookAuthors.Select(x => x.Author.FirstName + " " + x.Author.LastName)))
               .ForMember(d => d.AuthorsIds, o => o.MapFrom(s => s.BookAuthors.Select(x => x.AuthorId)))
               .ForMember(d => d.FormatsNames, o => o.MapFrom(s => s.BookFormats.Select(x => x.FileFormat.Name)))
               .ForMember(d => d.FormatsFilePaths, o => o.MapFrom(s => s.BookFormats.Select(x => x.FilePath)))
               .ForMember(d => d.Rating, o => o.MapFrom(s => s.RatingBooks.Select(x => x.ValueRating).DefaultIfEmpty(0).Average()));
            AutoMapper.Mapper.CreateMap<BookDto, Book>();

        }
    }
}
