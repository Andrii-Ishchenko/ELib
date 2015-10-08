using ELib.DAL.Infrastructure.Abstract;

namespace ELib.Tests.Fake
{
    public class FakeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new FakeUnitofWork();
        }
    }
}
