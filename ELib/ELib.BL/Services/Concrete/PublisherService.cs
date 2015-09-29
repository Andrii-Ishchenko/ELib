using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class PublisherService:  BaseService<Publisher,PublisherDto>, IPublisherService
    {
        public PublisherService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {

        }

        public IEnumerable<PublisherDto> GetAll(string query, int pageCount, int pageNumb)
        {
            Expression<Func<Publisher, bool>> filter = (string.IsNullOrEmpty(query)) ? SearchService<Publisher>.True : buildFullExpression(query);
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<PublisherDto>();
                var repository = uow.Repository<Publisher>();
                var entities = repository .Get(filter: filter, skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;
                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<PublisherDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        private static Expression<Func<Publisher, bool>> buildFullExpression(string query)
        {
            Expression<Func<Publisher, bool>> filter = SearchService<Publisher>.False;
            string[] words = query.Split(' ');
            foreach (string word in words)
            {
                filter = SearchService<Publisher>.filterOr(filter, p => p.Name.Contains(word));
            }
            return filter;
        }
    }
}
