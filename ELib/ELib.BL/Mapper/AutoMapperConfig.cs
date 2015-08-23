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
            configureCommentMapping();
            configureRatingBook();
            configureRatingComment();
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
               .ForMember(d => d.Rating, o => o.MapFrom(s => s.RatingBooks.Select(x => x.ValueRating).DefaultIfEmpty(0).Average()));
            AutoMapper.Mapper.CreateMap<BookDto, Book>();

        }

        private static void configureCommentMapping()
        {
            AutoMapper.Mapper.CreateMap<Comment, CommentDto>();
            AutoMapper.Mapper.CreateMap<CommentDto, Comment>();
        }

        private static void configureRatingBook()
        {
            AutoMapper.Mapper.CreateMap<RatingBook, RatingBookDto>();
            AutoMapper.Mapper.CreateMap<RatingBookDto, RatingBook>();
        }

        private static void configureRatingComment()
        {
            AutoMapper.Mapper.CreateMap<RatingComment, RatingCommentDto>();
            AutoMapper.Mapper.CreateMap<RatingCommentDto, RatingComment>();
        }
    }
}
