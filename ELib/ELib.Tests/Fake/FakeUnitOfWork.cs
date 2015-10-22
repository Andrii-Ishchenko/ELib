using ELib.DAL.Infrastructure.Abstract;
using System;
using ELib.DAL.Repositories.Abstract;

namespace ELib.Tests.Fake
{
    public class FakeUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FakeContext _context;
        private bool _disposed = false;

        public FakeUnitOfWork(FakeContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public IBaseRepository<T> Repository<T>() where T : class
        {
            return new FakeRepository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
