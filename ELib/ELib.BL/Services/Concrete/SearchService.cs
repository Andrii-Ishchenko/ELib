using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqKit;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public static class SearchService<TEntity>
        where TEntity : class
    {
        public static Expression<Func<TEntity, bool>>True
        {
            get
            {
                return PredicateBuilder.True<TEntity>();
            }
        }

        public static Expression<Func<TEntity, bool>> False
        {
            get
            {
                return PredicateBuilder.False<TEntity>();
            }
        }

        public static Expression<Func<TEntity, bool>> filterAnd(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> byQery)
        {
            filter = (byQery == null) ? filter : filter.And(byQery);
            return filter;
        }

        public static Expression<Func<TEntity, bool>> filterOr(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> byQery)
        {
            filter = (byQery == null) ? filter : filter.Or(byQery);
            return filter;
        }
    }
}