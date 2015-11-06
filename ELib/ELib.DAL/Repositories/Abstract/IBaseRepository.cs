using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.Domain.Entities.Abstract;

namespace ELib.DAL.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntityState
    {
        int TotalCount { get; }

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int skipCount = 0, int topCount = 100);

        TEntity GetById(object id);

        TEntity Insert(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);

        void Update(TEntity entity);

        void AddOrUpdate(TEntity entity);
    }
}
