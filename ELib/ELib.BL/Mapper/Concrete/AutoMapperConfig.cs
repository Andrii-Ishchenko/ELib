using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System.Linq;
using System;
using ELib.BL.Services.Concrete;
using ELib.DAL.Infrastructure.Concrete;
using System.Collections.Generic;
using AutoMapper;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {

            configureGenreMapping();
            configureSubgenreMapping();
            configurePublisherMapping();
            configureBookMapping();
            configureAuthorMapping();
            configureCommentMapping();
            configureRatingBook();
            configureRatingComment();
            configurePersonMapping();
            configureCurrentPerson();
            configureLanguageMapping();
            configureBookInstanceMapping();
            configureCategoryMapping();
            configureCategoryNestedMapping();
            configureAuthorListMapping();
        }

        private static void configureAuthorListMapping()
        {
            AutoMapper.Mapper.CreateMap<AuthorDto, AuthorListDto>()
                .ForMember(c=>c.Name,o=>o.MapFrom(u=>u.FirstName+" "+u.LastName));
            AutoMapper.Mapper.CreateMap<AuthorListDto, AuthorDto>();
        }

        private static void configureCategoryNestedMapping()
        {
            AutoMapper.Mapper.CreateMap<CategoryDto, CategoryNestedDto>();
            AutoMapper.Mapper.CreateMap<CategoryNestedDto, CategoryDto>();
        }

        private static void configureCategoryMapping()
        {
            AutoMapper.Mapper.CreateMap<Category, CategoryDto>();
            AutoMapper.Mapper.CreateMap<Category, CategoryDto>();

        }
        private static void configureSubgenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Subgenre, SubgenreDto>();
            AutoMapper.Mapper.CreateMap<SubgenreDto, Subgenre>();
        }

        private static void configurePersonMapping()
        {
            AutoMapper.Mapper.CreateMap<Person, PersonDto>();
            AutoMapper.Mapper.CreateMap<PersonDto, Person>();
        }

        private static void configureCurrentPerson()
        {
            UnitOfWorkFactory uowf = new UnitOfWorkFactory();
            FileService fs = new FileService(uowf);

            AutoMapper.Mapper.CreateMap<Person, CurrentPersonDto>()
                .ForMember(d => d.Email, o => o.MapFrom(p => p.ApplicationUser.Email))
                .ForMember(d => d.UserName, o => o.MapFrom(p => p.ApplicationUser.UserName));              
            AutoMapper.Mapper.CreateMap<CurrentPersonDto, Person>();
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
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                  .ForMember(d => d.GenresNames, o => o.MapFrom(s => s.BookGenres == null ? null : s.BookGenres.Select(x => x.Genre.Name)))
                  .ForMember(d => d.GenresIds, o => o.MapFrom(s => s.BookGenres == null ? null : s.BookGenres.Select(x => x.GenreId)))
                  .ForMember(d => d.Rating, o => o.MapFrom(s => s.SumRatingValue))
                  .ForMember(d => d.LanguageName, o => o.MapFrom(s => s.Language.Name))
                  .ForMember(d => d.Language1Name, o => o.MapFrom(s => s.Language1.Name))
                  .ForMember(d => d.PublisherName, o => o.MapFrom(s => s.Publisher.Name))
                  .ForMember(d => d.SubgenreName, o => o.MapFrom(s => s.Subgenre.Name))
                  .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name));
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

        private static void configureLanguageMapping()
        {
            AutoMapper.Mapper.CreateMap<Language, LanguageDto>();
            AutoMapper.Mapper.CreateMap<LanguageDto, Language>();
        }

        private static void configureBookInstanceMapping()
        {
            AutoMapper.Mapper.CreateMap<BookInstance, BookInstanceDto>();
            AutoMapper.Mapper.CreateMap<BookInstanceDto, BookInstance>();

            AutoMapper.Mapper.CreateMap<ICollection<BookInstance>, ICollection<BookInstanceDto>>().ConstructUsing((ResolutionContext rc) => new List<BookInstanceDto>());
            AutoMapper.Mapper.CreateMap<ICollection<BookInstanceDto>, ICollection<BookInstance>>().ConstructUsing((ResolutionContext rc) => new List<BookInstance>());
        }
    }
}
