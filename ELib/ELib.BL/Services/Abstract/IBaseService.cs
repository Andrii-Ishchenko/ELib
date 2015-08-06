using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ELib.BL.Services.Abstract
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);

        void Update(TEntity entity);
    }
}
