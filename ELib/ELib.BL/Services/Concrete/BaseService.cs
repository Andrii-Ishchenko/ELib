using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BaseService<TEntity, TEntityDto> : IBaseService<TEntity, TEntityDto> 
        where TEntityDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWorkFactory _factory;

        public BaseService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public int TotalCount
        {
            get
            {
                using (var uow = _factory.Create())
                {
                    return uow.Repository<TEntity>().TotalCount;
                }
            }
        }

        public void Delete(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToDelete = AutoMapper.Mapper.Map<TEntity>(entity);
                uow.Repository<TEntity>().Delete(entityToDelete);
                uow.Save();
            }
        }

        public void DeleteById(object id)
        {
            using (var uow = _factory.Create())
            {
                uow.Repository<TEntity>().DeleteById(id);
                uow.Save();
            }
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<TEntityDto>();
                var entities = uow.Repository<TEntity>().Get();

                foreach(var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<TEntityDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public TEntityDto GetById(object id)
        {
            using (var uow = _factory.Create())
            {
                var entity = uow.Repository<TEntity>().GetById(id);
                var entityDto = AutoMapper.Mapper.Map<TEntityDto>(entity);
                return entityDto;
            }
        }

        public void Insert(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToInsert = AutoMapper.Mapper.Map<TEntity>(entity);
                uow.Repository<TEntity>().Insert(entityToInsert);
                uow.Save();
                AutoMapper.Mapper.Map<TEntity, TEntityDto>(entityToInsert, entity);
            }
        }

        public void Update(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToUpdate = AutoMapper.Mapper.Map<TEntity>(entity);
                uow.Repository<TEntity>().Update(entityToUpdate);
                uow.Save();
            }
        }
    }
}
