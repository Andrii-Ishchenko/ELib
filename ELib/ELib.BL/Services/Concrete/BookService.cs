using System;
using System.Collections.Generic;
using System.Linq;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using LinqKit;

namespace ELib.BL.Services.Concrete
{
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        public BookService(IUnitOfWorkFactory factory)
            : base(factory)
        {

        }
        public IEnumerable<BookDto> GetForAuthor(int idAuthor)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var entities = uow.Repository<BookAuthor>().Get(x => x.AuthorId == idAuthor).Select(y => y.Book).OrderByDescending(rating => rating.SumRatingValue);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetBooksForPublisher(int id)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                //var entities = uow.Repository<Book>().Get(x => x.PublisherId == id).OrderByDescending(rating => rating.SumRatingValue);
                var entities = uow.Repository<Book>().Get(x => x.PublisherId == id);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetAll(string query, int pageCount, int pageNumb)
        {
            Expression<Func<Book, bool>> expression = buildExpression(query);
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var repository = uow.Repository<Book>();
                var entities = repository.Get(filter: expression, skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;
                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        private Expression<Func<Book, bool>> buildExpression(string query)
        {
            if (query == null)
                return PredicateBuilder.True<Book>();

            string[] words = query.Split(' ');
            Expression<Func<Book, bool>> filter = PredicateBuilder.False<Book>();
            foreach (string word in words)
            {
                Expression<Func<Book, bool>> searchrByTitle = x => x.Title.Contains(word);
                filter = filterOr(filter, searchrByTitle);

                Expression<Func<Book, bool>> searchByAuthor = (x) => x.BookAuthors.AsQueryable().Where(a => (a.Author.LastName + a.Author.FirstName).Contains(word)).Count() > 0;
                filter = filterOr(filter, searchByAuthor);

                Expression<Func<Book, bool>> searchByGenre = (x) => x.BookGenres.AsQueryable().Where(g => g.Genre.Name.Contains(word)).Count() > 0;
                filter = filterOr(filter, searchByGenre);

                Expression<Func<Book, bool>> searchByPublisher = x => x.Publisher.Name.Contains(word);
                filter = filterOr(filter, searchByPublisher);

                Expression<Func<Book, bool>> searchBySubgenre = x => x.Subgenre.Name.Contains(word);
                filter = filterOr(filter, searchBySubgenre);

                Expression<Func<Book, bool>> searchByYear = x => x.PublishYear.HasValue && x.PublishYear.Value.Year.ToString().Contains(word);
                filter = filterOr(filter, searchByYear);

            }

            return filter;
        }


        private static Expression<Func<Book, bool>> filterAdd(Expression<Func<Book, bool>> filter, Expression<Func<Book, bool>> byQery)
        {
            filter = (byQery == null) ? filter : filter.And(byQery);
            return filter;
        }

        private static Expression<Func<Book, bool>> filterOr(Expression<Func<Book, bool>> filter, Expression<Func<Book, bool>> byQery)
        {
            filter = (byQery == null) ? filter : filter.Or(byQery);
            return filter;
        }
    }
}