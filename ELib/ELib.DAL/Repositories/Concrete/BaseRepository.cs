using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.DAL.Infrastructure.Concrete;
using System.Data.Entity;
using ELib.DAL.Repositories.Abstract;
using ELib.Domain.Entities;
using LinqKit;

namespace ELib.DAL.Repositories.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private int _totalCount;
        internal ELibDbContext context;
        internal DbSet<TEntity> dbSet;

        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
        }

        public BaseRepository(ELibDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Delete(TEntity entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void DeleteById(object id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int skipCount = 0, int topCount = 0)
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            _totalCount = query.Count<TEntity>();

            query = (orderBy != null) ? orderBy(query) : query;
            if (skipCount >= 0 && topCount > 0)
            {
                return (query.AsEnumerable()).Skip(skipCount).Take(topCount);
            }
            return query.AsEnumerable();
            
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
