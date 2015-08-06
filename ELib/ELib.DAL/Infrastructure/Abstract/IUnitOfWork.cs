using ELib.DAL.Repositories.Abstract;
using System;

namespace ELib.DAL.Infrastructure.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IBaseRepository<T> Repository<T>() where T : class;
    }
}
