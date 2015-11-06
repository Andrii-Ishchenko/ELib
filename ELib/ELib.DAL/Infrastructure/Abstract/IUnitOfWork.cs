using ELib.DAL.Repositories.Abstract;
using ELib.Domain.Entities.Abstract;
using System;

namespace ELib.DAL.Infrastructure.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IBaseRepository<T> Repository<T>() where T : class, IEntityState;
    }
}
