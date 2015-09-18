using System;
using System.Collections.Generic;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ELib.BL.Services.Concrete
{
    public class AuthorService : BaseService<Author, AuthorDto>, IAuthorService
    {
        public AuthorService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {
        }
        public IEnumerable<AuthorDto> GetAll(string query, int pageNumb, int pageCount)
        {
            Expression<Func<Author, bool>> filter = (string.IsNullOrEmpty(query)) ? SearchService<Author>.True : buildFullExpression(query);
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<AuthorDto>();
                var repository = uow.Repository<Author>();
                var entities = repository.Get(filter: filter, skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;
                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<AuthorDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
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
