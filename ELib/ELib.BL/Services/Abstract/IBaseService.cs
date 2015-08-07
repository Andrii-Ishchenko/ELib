using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ELib.BL.Services.Abstract
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll();
    }
}
