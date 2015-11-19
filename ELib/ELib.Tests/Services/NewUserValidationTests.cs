using ELib.Tests.Fake;
using ELib.Web.Models;
using NUnit.Framework;
using System.Net;

namespace ELib.Tests.Services
{
    [TestFixture]
    public class NewUserValidationTests
    {
        private readonly FakeProfileService _fakeProfileService;
        private readonly FakeSendEmailService _fakeSendEmailService;

        public NewUserValidationTests()
        {
            _fakeProfileService = new FakeProfileService();
            _fakeSendEmailService = new FakeSendEmailService();
        }

        [Test]
        public void GetError_When_Set_Spases_To_UserName()
        {
            // Arrange
            var accountController = new ELib.Web.ApiControllers.AccountController(_fakeProfileService, _fakeSendEmailService);
            var model = new RegisterBindingModel() { UserName = "r r r", Email = "rrr@rrr.com", Password = "eee444%%%" };
            var msgRequest = "Model State Is Not Valid";

            // Act
            var msg = accountController.Register(model);

            // Assert
            Assert.AreEqual(msg.Result.RequestMessage, msgRequest);
            Assert.AreEqual(msg.Result.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
