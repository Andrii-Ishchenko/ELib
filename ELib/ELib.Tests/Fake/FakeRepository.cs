using ELib.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace ELib.Tests.Fake
{
    public class FakeRepository<T> : IBaseRepository<T> where T : class
    {
        private int _totalCount;
        private readonly FakeContext _context;
        internal DbSet<T> dbSet;

        public FakeRepository(FakeContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
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
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int skipCount = 0, int topCount = 0)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }

            _totalCount = query.Count<T>();

            query = (orderBy != null) ? orderBy(query) : query;
            if (skipCount >= 0 && topCount > 0)
            {
                return (query.AsEnumerable()).Skip(skipCount).Take(topCount);
            }
            return query.AsEnumerable();
        }
        
        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
