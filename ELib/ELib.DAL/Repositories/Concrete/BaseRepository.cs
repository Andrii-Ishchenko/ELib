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
            IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int skipCount = 0, int topCount = 0)
        {
            context.Configuration.LazyLoadingEnabled = false;
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            _totalCount = query.Count<TEntity>();

            if (orderBy != null) {
                query = orderBy(query);
                if (skipCount == 0 && topCount > 0)
                {
                    query = query.Take(topCount);
                }

                else if (orderBy != null && skipCount > 0 && topCount > 0)
                {
                    query = query.Skip(skipCount).Take(topCount);
                }
            }

            else if (orderBy == null && skipCount == 0 && topCount > 0)
            {
                query = query.Take(topCount);
            }

            else if (orderBy == null && skipCount > 0 && topCount > 0)
            {
                return query.AsEnumerable().Skip(skipCount).Take(topCount);
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
