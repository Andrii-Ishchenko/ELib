﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper.Abstract;
using System.Linq;

namespace ELib.BL.Services.Concrete
{
    public class PublisherService : BaseService<Publisher, PublisherDto>, IPublisherService
    {
        public PublisherService(IUnitOfWorkFactory factory, IMapper<Publisher, PublisherDto> mapper)
            : base(factory, mapper)
        { }

        public IEnumerable<PublisherDto> GetAll(string query, int pageCount, int pageNumb, string orderBy, string orderingDirection)
        {
            Expression<Func<Publisher, bool>> filter = (string.IsNullOrEmpty(query)) ? SearchService<Publisher>.True : buildFullExpression(query);
            Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> orderExp = buildOrderExpression(orderBy, orderingDirection);
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<PublisherDto>();
                var repository = uow.Repository<Publisher>();
                var entities = repository.Get(filter: filter, orderBy: orderExp, skipCount: pageCount * (pageNumb - 1), topCount: pageCount);
                TotalCount = repository.TotalCount;
                foreach (var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }
                return entitiesDto;
            }
        }

        private Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> buildOrderExpression(string orderBy, string orderingDirection)
        {
            bool isOrderASC = orderingDirection == "ASC";
            if (isOrderASC)
            {
                switch (orderBy)
                {
                    // publisher.Name is not null
                    case "Name": return q => q.OrderBy(p => p.Name).ThenBy(p=>p.Id);
                    default: break;
                }
            }
            else
            {
                switch (orderBy)
                {
                    // publisher.Name is not null
                    case "Name": return q => q.OrderByDescending(p => p.Name).ThenByDescending(p=>p.Id);
                    default: break;
                }
            }
            //default 
            return query => query.OrderBy(b => b.Name);

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
