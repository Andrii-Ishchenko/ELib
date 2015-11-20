using ELib.BL.Services.Abstract;
using System;

namespace ELib.Tests.Fake
{
    public class FakeSendEmailService : ISendEmailService
    {
        public void SendEmail(string email, string login)
        {
            throw new NotImplementedException();
        }
    }
}
