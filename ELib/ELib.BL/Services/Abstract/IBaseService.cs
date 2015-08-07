using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ELib.BL.Services.Abstract
{
    public interface IBaseService<TEntity, TEntityDto> 
        where TEntity : class
        where TEntityDto : class
    {
        TEntityDto GetById(object id);

        void Insert(TEntityDto entity);

        void Delete(TEntityDto entity);

        void DeleteById(object id);

        void Update(TEntityDto entity);

        IEnumerable<TEntityDto> GetAll();
    }
}
