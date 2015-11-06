using System;
using ELib.DAL.Infrastructure.Abstract;
using ELib.DAL.Repositories.Abstract;
using ELib.DAL.Repositories.Concrete;
using ELib.Domain.Entities.Abstract;

namespace ELib.DAL.Infrastructure.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ELibDbContext _context = new ELibDbContext();

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IBaseRepository<T> Repository<T>() where T : class, IEntityState
        {
            return new BaseRepository<T>(_context);
        }
    }
}
