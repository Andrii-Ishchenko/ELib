using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System.Linq;
using System;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            configureGenreMapping();
            configurePublisherMapping();
            configureBookMapping();
            configureAuthorMapping();
            configureCommentMapping();
            configureRatingBook();
            configureRatingComment();
            configurePersonMapping();
        }

        private static void configurePersonMapping()
        {
            AutoMapper.Mapper.CreateMap<Person, PersonDto>();
        }

        private static void configureGenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Genre, GenreDto>();
            AutoMapper.Mapper.CreateMap<GenreDto, Genre>();          
        }

        private static void configurePublisherMapping()
        {
            AutoMapper.Mapper.CreateMap<Publisher, PublisherDto>();
            AutoMapper.Mapper.CreateMap<PublisherDto, Publisher>();
        }

        private static void configureBookMapping()
        {
            AutoMapper.Mapper.CreateMap<Book, BookDto>()
              .ForMember(d => d.Authors, o => o.MapFrom(s => s.BookAuthors.Select(x => x.Author.FirstName + " " + x.Author.LastName)))
              .ForMember(d => d.AuthorsIds, o => o.MapFrom(s => s.BookAuthors.Select(x => x.AuthorId)))
              .ForMember(d => d.GenresNames, o => o.MapFrom(s => s.BookGenres.Select(x => x.Genre.Name)))
              .ForMember(d => d.GenresIds, o => o.MapFrom(s => s.BookGenres.Select(x => x.GenreId)))
              .ForMember(d => d.Rating, o => o.MapFrom(s => s.RatingBooks.Select(x => x.ValueRating).DefaultIfEmpty(0).Average()));
            AutoMapper.Mapper.CreateMap<BookDto, Book>();
        }

        private static void configureAuthorMapping()
        {
            AutoMapper.Mapper.CreateMap<Author, AuthorDto>();
            AutoMapper.Mapper.CreateMap<AuthorDto, Author>();
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
