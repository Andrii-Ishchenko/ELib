using System;
using System.Collections.Generic;
using System.Linq;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        public BookService(IUnitOfWorkFactory factory, IMapper<Book, BookDto> mapper)
            : base(factory, mapper)
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
                    var entityDto = _mapper.Map(item);
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
                var entities = uow.Repository<Book>().Get(x => x.PublisherId == id);

                foreach (var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetNewBooks(int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var repository = uow.Repository<Book>();
                var entities = repository.Get(orderBy: q => q.OrderByDescending(d => d.AdditionDate), skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;

                foreach (var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetBooksWithHighestRating(int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var repository = uow.Repository<Book>();
                var entities = repository.Get(orderBy: q => q.OrderByDescending(d => d.SumRatingValue), skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;

                foreach (var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }
        public IEnumerable<BookDto> GetAll(SearchDto searchDto, int pageCount, int pageNumb)
        {
            Expression<Func<Book, bool>> filter;
            var byParameter = buildFilterExpression(searchDto);
            if (searchDto.Query != null)
            {
                filter = buildFullExpression(searchDto.Query);
                if (byParameter != SearchService<Book>.False)
                    filter = SearchService<Book>.filterAnd(filter, byParameter);
            }
            else
            {
                filter = byParameter;
            }  
                                                                        
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var repository = uow.Repository<Book>();
                var entities = repository.Get(filter: filter, skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;
                foreach (var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        private Expression<Func<Book, bool>> buildFilterExpression(SearchDto searchDto)
        {
            Expression<Func<Book, bool>> filter =SearchService<Book>.True;
            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                Expression<Func<Book, bool>> searchrByTitle = x => x.Title.Contains(searchDto.Title);
                filter = SearchService<Book>.filterAnd(filter, searchrByTitle);
            }

            if (!string.IsNullOrEmpty(searchDto.AuthorName))
            {
                Expression<Func<Book, bool>> searchByAuthor = (x) => x.BookAuthors.AsQueryable().Where(a => (a.Author.LastName + a.Author.FirstName).Contains(searchDto.AuthorName)).Count() > 0;
                filter = SearchService<Book>.filterAnd(filter, searchByAuthor);
            }

            if (!string.IsNullOrEmpty(searchDto.Genre))
            {
                Expression<Func<Book, bool>> searchByGenre = (x) => x.BookGenres.AsQueryable().Where(g => g.Genre.Name.Contains(searchDto.Genre)).Count() > 0;
                filter = SearchService<Book>.filterAnd(filter, searchByGenre);
            }

            if (!string.IsNullOrEmpty(searchDto.Subgenre))
            {
                Expression<Func<Book, bool>> searchBySubgenre = x => x.Subgenre.Name.Contains(searchDto.Subgenre);
                filter = SearchService<Book>.filterAnd(filter, searchBySubgenre);
            }

            if (!string.IsNullOrEmpty(searchDto.Publisher))
            {
                Expression<Func<Book, bool>> searchByPublisher = x => x.Publisher.Name.Contains(searchDto.Publisher);
                filter = SearchService<Book>.filterAnd(filter, searchByPublisher);
            }

            if (searchDto.GenreId > -1)
            {
                Expression<Func<Book, bool>> searchByGenreId = (x) => x.BookGenres.AsQueryable().Where(g => (g.Id > -1 && g.Id == searchDto.GenreId)).Count() > 0;
                filter = SearchService<Book>.filterAnd(filter, searchByGenreId);
            }

            if (searchDto.Year > 0)
            {
                Expression<Func<Book, bool>> searchByYear = x => x.PublishYear > 0 && x.PublishYear == searchDto.Year;
                filter = SearchService<Book>.filterAnd(filter, searchByYear);
            }
            return filter;
        }

        private Expression<Func<Book, bool>> buildFullExpression(string query)
        {
            if (string.IsNullOrEmpty(query))
                return SearchService<Book>.True;

            string[] words = query.Split(' ');
            Expression<Func<Book, bool>> filter =SearchService<Book>.False;
            foreach (string word in words)
            {
                Expression<Func<Book, bool>> searchrByTitle = x => x.Title.Contains(word);
                filter = SearchService<Book>.filterOr(filter, searchrByTitle);

                Expression<Func<Book, bool>> searchByAuthor = (x) => x.BookAuthors.AsQueryable().Where(a => (a.Author.LastName + a.Author.FirstName).Contains(word)).Count() > 0;
                filter = SearchService<Book>.filterOr(filter, searchByAuthor);

                Expression<Func<Book, bool>> searchByGenre = (x) => x.BookGenres.AsQueryable().Where(g => g.Genre.Name.Contains(word)).Count() > 0;
                filter = SearchService<Book>.filterOr(filter, searchByGenre);

                Expression<Func<Book, bool>> searchByPublisher = x => x.Publisher.Name.Contains(word);
                filter = SearchService<Book>.filterOr(filter, searchByPublisher);

                Expression<Func<Book, bool>> searchBySubgenre = x => x.Subgenre.Name.Contains(word);
                filter = SearchService<Book>.filterOr(filter, searchBySubgenre);

                Expression<Func<Book, bool>> searchByGenreId = (x) => x.BookGenres.AsQueryable().Where(g => (g.Id > -1 && g.Id.ToString().Contains(word))).Count() > 0;
                filter = SearchService<Book>.filterOr(filter, searchByGenreId);

                Expression<Func<Book, bool>> searchByYear = x => x.PublishYear > 0 && x.PublishYear.ToString().Contains(word);
                filter = SearchService<Book>.filterOr(filter, searchByYear);

            }

            return filter;
        }        
    }
}