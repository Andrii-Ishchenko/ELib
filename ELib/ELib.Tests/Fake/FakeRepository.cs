using ELib.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using ELib.Domain.Entities.Abstract;

namespace ELib.Tests.Fake
{
    public class FakeRepository<T> : IBaseRepository<T> where T : class, IEntityState
    {
        private int _totalCount;

        public readonly List<T> Data;

        public FakeRepository(params T[] data)
        {
            Data = new List<T>(data);
        }

        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
        }

        public void Delete(T entity)
        {
            // Data.Remove(entity);
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
         IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int skipCount = 0, int topCount = 100)
        {

            IQueryable<T> query = Data.AsQueryable();
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }

            _totalCount = query.Count<T>();

            if (skipCount >= 0 && topCount > 0)
            {
                if (orderBy != null)
                {
                    return orderBy(query).Skip(skipCount).Take(topCount);
                }
                else return query.AsEnumerable().Skip(skipCount).Take(topCount);
            }
            else
            { throw new ArgumentException("skipCount or topCount is negative"); }

        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            //Data.Add(entity);
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
