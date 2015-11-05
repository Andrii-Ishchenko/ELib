using System;
using NUnit.Framework;
using ELib.Domain.Entities;
using ELib.BL.Services.Concrete;
using ELib.Tests.Fake;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Mapper.Concrete;
using ELib.BL.DtoEntities;
using System.Linq;
namespace ELib.Tests.Services
{
    [TestFixture]
    public class PublisherServiceTests
    {
        private readonly FakeUnitOfWorkFactory _fakeUnitOfWorkFactory;
        private readonly FakeRepository<Publisher> _fakePublisherRepository;
        protected readonly IMapper<Publisher, PublisherDto> _mapper;

        public PublisherServiceTests()
        {
            _mapper = new PublisherMapper();
            _fakePublisherRepository = new FakeRepository<Publisher>();

            _fakeUnitOfWorkFactory = new FakeUnitOfWorkFactory(
                uow =>
                {
                    uow.SetRepository(_fakePublisherRepository);
                });
        }

        [SetUp]
        public void TestSetup()
        {
            var publisher1 = new Publisher { Id = 1, Name = "Аверс" };
            var publisher2 = new Publisher { Id = 2, Name = "Oma-Book" };
            var publisher3 = new Publisher { Id = 3, Name = "Фактор" };
            var publisher4 = new Publisher { Id = 4, Name = "Просвита" };
            var publisher5 = new Publisher { Id = 5, Name = "FriedensBote" };
            var publisher6 = new Publisher { Id = 6, Name = "АФІША" };
            var publisher7 = new Publisher { Id = 7, Name = "Диалектика" };
            var publisher8 = new Publisher { Id = 8, Name = "41Аверс" };
            var publisher9 = new Publisher { Id = 9, Name = "Лотос" };
            var publisher10 = new Publisher { Id = 10, Name = "Аверс98" };

            _fakePublisherRepository.Data.AddRange(new[] { publisher1, publisher2, publisher3, publisher4, publisher5, publisher6, publisher7, publisher8, publisher9, publisher10 });
        }

        // This method runs after each test.
        [TearDown]
        public void TestDown()
        {
            // Clean up data in our fake dependencies.
            _fakePublisherRepository.Data.Clear();
        }

        [Test]
        public void GetAll_When_set_pageCount_Then_get_count_publishers()
        {
            // Arrange
            var service = new PublisherService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var publishers1 = service.GetAll(query: null, pageNumb: 1, pageCount: 5, orderBy: "Name", orderingDirection: "ASC");
            var publishers2 = service.GetAll(query: null, pageNumb: 2, pageCount: 5, orderBy: "Name", orderingDirection: "ASC");
            var publishers3 = service.GetAll(query: null, pageNumb: 1, pageCount: 4, orderBy: "Name", orderingDirection: "ASC");
            var publishers4 = service.GetAll(query: null, pageNumb: 2, pageCount: 4, orderBy: "Name", orderingDirection: "DESC");
            var publishers5 = service.GetAll(query: null, pageNumb: 3, pageCount: 2, orderBy: "Name", orderingDirection: "DESC");
            var publishers6 = service.GetAll(query: null, pageNumb: 1, pageCount: 5, orderBy: "Name", orderingDirection: "DESC");

            // Assert
            Assert.AreEqual(5, publishers1.Count());
            Assert.AreEqual(5, publishers2.Count());
            Assert.AreEqual(4, publishers3.Count());
            Assert.AreEqual(4, publishers4.Count());
            Assert.AreEqual(2, publishers5.Count());
            Assert.AreEqual(5, publishers6.Count());
        }

        [Test]
        public void GetAll_When_set_pageCount_and_authors_in_base_less_Then_get_less_count_publishers()
        {
            // Arrange
            var service = new PublisherService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var publishers1 = service.GetAll(query: null, pageNumb: 2, pageCount: 10, orderBy: "Name", orderingDirection: "ASC");
            var publishers2 = service.GetAll(query: null, pageNumb: 1, pageCount: 15, orderBy: "Name", orderingDirection: "ASC");
            var publishers3 = service.GetAll(query: null, pageNumb: 5, pageCount: 4, orderBy: "Name", orderingDirection: "ASC");
            var publishers4 = service.GetAll(query: null, pageNumb: 1, pageCount: 45, orderBy: "Name", orderingDirection: "DESC");
            var publishers5 = service.GetAll(query: null, pageNumb: 3, pageCount: 4, orderBy: "Name", orderingDirection: "DESC");
            var publishers6 = service.GetAll(query: null, pageNumb: 1, pageCount: 25, orderBy: "Name", orderingDirection: "DESC");

            // Assert
            Assert.AreEqual(0, publishers1.Count());
            Assert.AreEqual(10, publishers2.Count());
            Assert.AreEqual(0, publishers3.Count());
            Assert.AreEqual(10, publishers4.Count());
            Assert.AreEqual(2, publishers5.Count());
            Assert.AreEqual(10, publishers6.Count());
        }

        [Test]
        public void GetAll_When_sorting_by_Name_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new PublisherService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var publishers1 = service.GetAll(query: null, pageNumb: 1, pageCount: 10, orderBy: "Name", orderingDirection: "ASC");
            var publishers2 = service.GetAll(query: null, pageNumb: 2, pageCount: 5, orderBy: "Name", orderingDirection: "ASC");
            var publishers3 = service.GetAll(query: null, pageNumb: 3, pageCount: 2, orderBy: "Name", orderingDirection: "ASC");

            // Assert
            Assert.That(publishers1.ToList()[0].Id, Is.EqualTo(8));
            Assert.That(publishers1.ToList()[9].Id, Is.EqualTo(3));
            Assert.That(publishers1.ToList()[4].Id, Is.EqualTo(10));

            Assert.That(publishers2.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(publishers2.ToList()[4].Id, Is.EqualTo(3));
            Assert.That(publishers2.ToList()[2].Id, Is.EqualTo(9));

            Assert.That(publishers3.ToList()[0].Id, Is.EqualTo(10));
            Assert.That(publishers3.ToList()[1].Id, Is.EqualTo(6));
        }

        [Test]
        public void GetAll_When_sorting_by_Name_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new PublisherService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var publishers1 = service.GetAll(query: null, pageNumb: 1, pageCount: 10, orderBy: "Name", orderingDirection: "DESC");
            var publishers2 = service.GetAll(query: null, pageNumb: 2, pageCount: 5, orderBy: "Name", orderingDirection: "DESC");
            var publishers3 = service.GetAll(query: null, pageNumb: 3, pageCount: 2, orderBy: "Name", orderingDirection: "DESC");

            // Assert
            Assert.That(publishers1.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(publishers1.ToList()[9].Id, Is.EqualTo(8));
            Assert.That(publishers1.ToList()[4].Id, Is.EqualTo(6));

            Assert.That(publishers2.ToList()[0].Id, Is.EqualTo(10));
            Assert.That(publishers2.ToList()[4].Id, Is.EqualTo(8));
            Assert.That(publishers2.ToList()[2].Id, Is.EqualTo(2));

            Assert.That(publishers3.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(publishers3.ToList()[1].Id, Is.EqualTo(10));
        }
    }
}
