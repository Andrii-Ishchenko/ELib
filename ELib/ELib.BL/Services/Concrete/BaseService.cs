using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWorkFactory _factory;

        public BaseService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void Delete(TEntity entity)
        {
            using (var uow = _factory.Create())
            {
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

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<TEntity>().Get(filter, orderBy, includeProperties);
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
    }
}
