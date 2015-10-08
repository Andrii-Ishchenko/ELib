using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Mapper.Concrete;
using ELib.BL.Services.Concrete;
using ELib.Domain.Entities;
using ELib.Tests.Fake;
using NUnit.Framework;
using FakeItEasy;
using ELib.DAL.Repositories.Abstract;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.Tests
{
    [TestFixture]
    public class BookServiceTest
    {
        private readonly IMapper<Book, BookDto> _mapper;


        public BookServiceTest()
        {
            _mapper = new BookMapper(new BookInstanceMapper(), new AuthorsListMapper());
        }

        [Test]
        public void GetBookById_When_Then()////////give correct name
        {
            // Assert
            var book = new BookDto() { Id = 1, Title = "FakeBook" };
            var repository = A.Fake<IBaseRepository<Book>>();

            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.Repository<Book>()).Returns(repository);

            var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
            A.CallTo(() => unitOfWorkFactory.Create()).Returns(unitOfWork);

            var service = new BookService(unitOfWorkFactory, _mapper);

            // Act and Assert
            Assert.AreEqual(book, service.GetById(book.Id));
        }
    }
}
