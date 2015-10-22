using ELib.DAL.Infrastructure.Abstract;

namespace ELib.Tests.Fake
{
    public class FakeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly FakeUnitOfWork _unitOfWork;

        public FakeUnitOfWorkFactory(FakeContext context)
        {
            _unitOfWork = new FakeUnitOfWork(context);
        }

        public IUnitOfWork Create()
        {
            return _unitOfWork;
        }
    }
}
