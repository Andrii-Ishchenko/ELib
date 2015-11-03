using ELib.DAL.Infrastructure.Abstract;
using System;

namespace ELib.Tests.Fake
{
    public class FakeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly FakeUnitOfWork _unitOfWork = new FakeUnitOfWork();

        public FakeUnitOfWorkFactory(Action<FakeUnitOfWork> constructUnitOfWork)
        {
            constructUnitOfWork(_unitOfWork);
        }
        public IUnitOfWork Create()
        {
            return _unitOfWork;
        }
    }
}
