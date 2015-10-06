using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ELib.DAL.Repositories.Abstract;
using ELib.DAL.Infrastructure.Concrete;

namespace ELib.Tests.Fake
{
    public class FakeRepository<T> : IBaseRepository<T> where T : class
    {
        protected Dictionary<int, T> _dictionary;
        protected Expression<Func<T, int>> _identityExpression;

        public FakeRepository(Expression<Func<T, int>> identityExpression,
        Dictionary<int, T> dictionary)
        {
            _identityExpression = identityExpression;
            _dictionary = dictionary;
        }

        public int TotalCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }      

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            _dictionary.Remove((int)id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int skipCount = 0, int topCount = 0)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            return !_dictionary.ContainsKey((int)id) ? null : _dictionary[(int)id];
        }

        public T Insert(T entity)
        {
            var newKey = _dictionary.Count == 0 ? 1 : _dictionary.Keys.Max() + 1;
            SetIdentityValue(entity, newKey);////????????????????????????
            _dictionary.Add(newKey, entity);
            return entity;
        }

        protected virtual void SetIdentityValue(T item, object value)
        {
            item.GetType().GetProperty(GetIdentityName()).SetValue(item, value, null);
        }

        protected virtual string GetIdentityName()
        {
            return ((MemberExpression)_identityExpression.Body).Member.Name;
        }

        public void Update(T entity)
        {
            var identityValue = GetIdentityValue(entity);
            if (!_dictionary.ContainsKey(identityValue))
                throw new Exception("Cannot update");
            _dictionary[identityValue] = entity;
        }

        protected virtual int GetIdentityValue(T item)
        {
            return (int)item.GetType().GetProperty(GetIdentityName()).GetValue(item, null);
        }
    }
}
