using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BaseService<TEntity, TEntityDto> : IBaseService<TEntityDto> 
        where TEntityDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWorkFactory _factory;

        public BaseService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void Delete(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToDelete = 
                uow.Repository<TEntity>().Delete(entity);
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

        public IEnumerable<TEntity> GetAll()
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<TEntity>().Get();
            }
        }

        public TEntity GetById(object id)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<TEntity>().GetById(id);
            }
        }

        public void Insert(TEntity entity)
        {
            using (var uow = _factory.Create())
            {
                uow.Repository<TEntity>().Insert(entity);
                uow.Save();
            }
        }

        public void Update(TEntity entity)
        {
            using (var uow = _factory.Create())
            {
                uow.Repository<TEntity>().Update(entity);
                uow.Save();
            }
        }

        void IBaseService<TEntity>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
