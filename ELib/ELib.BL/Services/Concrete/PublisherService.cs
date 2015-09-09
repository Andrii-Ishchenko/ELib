using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<PublisherDto> GetAll(int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<PublisherDto>();

                var entities = uow.Repository<Publisher>().Get(skipCount: pageCount * (pageNumb - 1), topCount: pageCount);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<PublisherDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }
    }
}
