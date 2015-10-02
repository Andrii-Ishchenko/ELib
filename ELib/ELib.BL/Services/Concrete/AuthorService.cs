using System;
using System.Collections.Generic;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using ELib.BL.Mapper;

namespace ELib.BL.Services.Concrete
{
    public class AuthorService : BaseService<Author, AuthorDto>, IAuthorService
    {
        public AuthorService(IUnitOfWorkFactory factory, IMapper<Author, AuthorDto> mapper)
            : base(factory, mapper)
        { }

        public IEnumerable<AuthorDto> GetAll(string query, string authorName, int year, int pageNumb, int pageCount)
        {
            Expression<Func<Author, bool>> filter;
            var byParameter = buildFilterExpression(query, authorName, year);
            if (query != null)
            {
                filter = buildFullExpression(query);
                if (byParameter != SearchService<Author>.False)
                    filter = SearchService<Author>.filterAnd(filter, byParameter);
            }
            else
            {
                filter = byParameter;
            }

            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<AuthorDto>();
                var repository = uow.Repository<Author>();
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

        private Expression<Func<Author, bool>> buildFilterExpression(string query, string authorName, int year)
        {
            Expression<Func<Author, bool>> filter = SearchService<Author>.True;
            if (!string.IsNullOrEmpty(authorName))
            {
                Expression<Func<Author, bool>> searchByAuthor = (x) => (x.FirstName + " " + x.LastName).Contains(authorName) || (x.LastName + " " + x.FirstName).Contains(authorName);
                filter = SearchService<Author>.filterAnd(filter, searchByAuthor);
            }

            if (year > 0)
            {
                Expression<Func<Author, bool>> searchByYear = x => x.DateOfBirth.Value.Year > 0 && x.DateOfBirth.Value.Year == year;
                filter = SearchService<Author>.filterAnd(filter, searchByYear);
            }
            return filter;
        }
        private Expression<Func<Author, bool>> buildFullExpression(string query)
        {
            Expression<Func<Author, bool>> filter = SearchService<Author>.False;
            string[] words = query.Split(' ');
            foreach (string word in words)
            {
                filter = SearchService<Author>.filterOr(filter, a => (a.FirstName + a.LastName).Contains(word));
                Expression<Func<Author, bool>> filterByBirthYear = a => a.DateOfBirth.HasValue && a.DateOfBirth.Value.Year.ToString().Contains(word);
                filter = SearchService<Author>.filterOr(filter, filterByBirthYear);
                Expression<Func<Author, bool>> filterByDeathYear = a => a.DeathDate.HasValue && a.DeathDate.Value.Year.ToString().Contains(word);
                filter = SearchService<Author>.filterOr(filter, filterByDeathYear);
            }
            return filter;
        }
    }
}