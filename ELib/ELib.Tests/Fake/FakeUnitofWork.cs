using ELib.DAL.Infrastructure.Abstract;
using ELib.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;

namespace ELib.Tests.Fake
{
    public class FakeUnitofWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
       // private readonly ELibDbContext _context = new ELibDbContext();

        public IBaseRepository<T> Repository<T>() where T : class
        {
            // object repository;
            // return _repositories.TryGetValue(typeof(T), out repository) ? (IBaseRepository<T>)repository : new FakeRepository<T>();
            // return new FakeRepository<T>(_context);
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
           // _context.SaveChanges();
        }
    }
}
