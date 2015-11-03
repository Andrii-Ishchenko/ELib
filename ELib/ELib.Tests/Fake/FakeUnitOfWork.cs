using ELib.DAL.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using ELib.DAL.Repositories.Abstract;

namespace ELib.Tests.Fake
{
    public class FakeUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public void SetRepository<T>(IBaseRepository<T> repository) where T : class
        {
            _repositories[typeof(T)] = repository;
        }

        public IBaseRepository<T> Repository<T>() where T : class
        {
            object repository;
            return _repositories.TryGetValue(typeof(T), out repository)
                       ? (IBaseRepository<T>)repository
                       : new FakeRepository<T>();
        }

        public void Dispose()
        {
        }

        public void Save()
        {


        }
    }
}
