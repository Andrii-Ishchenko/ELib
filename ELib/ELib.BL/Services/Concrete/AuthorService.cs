using System;
using System.Collections.Generic;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Concrete
{
    public class AuthorService : BaseService<Author, AuthorDto>, IAuthorService
    {
        public AuthorService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {

        }

        public IEnumerable<AuthorDto> GetAll(int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<AuthorDto>();

                var entities = uow.Repository<Author>().Get(skipCount: pageCount * (pageNumb - 1), topCount: pageCount);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<AuthorDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }
    }
}
