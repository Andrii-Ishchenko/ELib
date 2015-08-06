using ELib.DAL.Infrastructure.Abstract;

namespace ELib.DAL.Infrastructure.Concrete
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}
