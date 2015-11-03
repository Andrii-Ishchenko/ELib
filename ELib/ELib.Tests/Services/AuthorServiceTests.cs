using System;
using NUnit.Framework;
using Moq;
using ELib.DAL.Repositories.Abstract;
using ELib.DAL.Repositories.Concrete;
using ELib.Domain.Entities;
using System.Collections.Generic;
using ELib.BL.Services.Abstract;
using ELib.BL.Services.Concrete;
using ELib.Tests.Fake;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Mapper.Concrete;
using ELib.BL.DtoEntities;
using ELib.DAL.Infrastructure.Concrete;
using System.Data.Entity;

using System.Linq;
using System.Linq.Expressions;
using FakeItEasy;

namespace ELib.Tests.Services
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private readonly FakeUnitOfWorkFactory _fakeUnitOfWorkFactory;
        private readonly FakeRepository<Author> _fakeAuthorRepository;
        protected readonly IMapper<Author, AuthorDto> _mapper;


        public AuthorServiceTests()
        {
            _mapper = new AuthorMapper();
            _fakeAuthorRepository = new FakeRepository<Author>();

            _fakeUnitOfWorkFactory = new FakeUnitOfWorkFactory(
                uow =>
                {
                    uow.SetRepository(_fakeAuthorRepository);
                });
        }


        [SetUp]
        public void TestSetup()
        {
            var author1 = new Author { Id = 1, FirstName = "Джефри", LastName = "Иванов", DateOfBirth = new DateTime(1990, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author2 = new Author { Id = 2, FirstName = "3456", LastName = "Аметов", DateOfBirth = new DateTime(1990, 7, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author3 = new Author { Id = 3, FirstName = "Nick", LastName = "12345", DateOfBirth = new DateTime(1990, 5, 29), DeathDate = new DateTime(2011, 6, 1) };
            var author4 = new Author { Id = 4, FirstName = "Дмитрий", LastName = "3Степаненко", DateOfBirth = new DateTime(1991, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author5 = new Author { Id = 5, FirstName = "Daniel", LastName = "Petrov", DateOfBirth = new DateTime(1790, 4, 1), DeathDate = new DateTime(2015, 6, 1) };
            var author6 = new Author { Id = 6, FirstName = "Daniel1", LastName = "Petrov2", DateOfBirth = new DateTime(1690, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author7 = new Author { Id = 7, FirstName = "5Николай", LastName = "Петренко", DateOfBirth = new DateTime(1537, 11, 5), DeathDate = new DateTime(2008, 6, 1) };
            var author8 = new Author { Id = 8, FirstName = "Иван", LastName = "6Рихтер", DateOfBirth = new DateTime(1450, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author9 = new Author { Id = 9, FirstName = "7Дмитрий", LastName = "Rihter", DateOfBirth = new DateTime(1911, 12, 31), DeathDate = new DateTime(2008, 6, 1) };
            var author10 = new Author { Id = 110, FirstName = "Alina", LastName = "Franko", DateOfBirth = new DateTime(1200, 9, 22), DeathDate = new DateTime(2008, 6, 1) };

            _fakeAuthorRepository.Data.AddRange(new[] { author1, author2, author3, author4, author5, author6, author7, author8, author9, author10 });

        }

        // This method runs after each test.
        [TearDown]
        public void TestDown()
        {
            // Clean up data in our fake dependencies.
            _fakeAuthorRepository.Data.Clear();

        }

        [Test]
        public void GetAll_When_set_pageCount_Then_get_count_authors()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "FirstName", "ASC", 0, pageNumb: 1, pageCount: 5);
            var authors2 = service.GetAll(null, null, "FirstName", "DESC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "LastName", "ASC", 0, pageNumb: 1, pageCount: 4);
            var authors4 = service.GetAll(null, null, "LastName", "DESC", 0, pageNumb: 2, pageCount: 4);
            var authors5 = service.GetAll(null, null, "DateOfBirth", "ASC", 0, pageNumb: 3, pageCount: 2);
            var authors6 = service.GetAll(null, null, "DateOfBirth", "DESC", 0, pageNumb: 1, pageCount: 5);

            // Assert
            Assert.AreEqual(5, authors1.Count());
            Assert.AreEqual(5, authors2.Count());
            Assert.AreEqual(4, authors3.Count());
            Assert.AreEqual(4, authors4.Count());
            Assert.AreEqual(2, authors5.Count());
            Assert.AreEqual(5, authors6.Count());
        }

        [Test]
        public void GetAll_When_set_pageCount_and_authors_in_base_less_Then_get_less_count_authors()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "FirstName", "ASC", 0, pageNumb: 2, pageCount: 10);
            var authors2 = service.GetAll(null, null, "FirstName", "DESC", 0, pageNumb: 1, pageCount: 15);
            var authors3 = service.GetAll(null, null, "LastName", "ASC", 0, pageNumb: 5, pageCount: 4);
            var authors4 = service.GetAll(null, null, "LastName", "DESC", 0, pageNumb: 1, pageCount: 45);
            var authors5 = service.GetAll(null, null, "DateOfBirth", "ASC", 0, pageNumb: 3, pageCount: 4);
            var authors6 = service.GetAll(null, null, "DateOfBirth", "DESC", 0, pageNumb: 1, pageCount: 25);

            // Assert
            Assert.AreEqual(0, authors1.Count());
            Assert.AreEqual(10, authors2.Count());
            Assert.AreEqual(0, authors3.Count());
            Assert.AreEqual(10, authors4.Count());
            Assert.AreEqual(2, authors5.Count());
            Assert.AreEqual(10, authors6.Count());
        }


        [Test]
        public void GetAll_When_sorting_by_FirstName_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "FirstName", "ASC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "FirstName", "ASC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "FirstName", "ASC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(2));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(8));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(5));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(8));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(1));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(6));
         
        }

        [Test]
        public void GetAll_When_sorting_by_FirstName_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "FirstName", "DESC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "FirstName", "DESC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "FirstName", "DESC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(8));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(2));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(6));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(2));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(9));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(5));

        }

        [Test]
        public void GetAll_When_sorting_by_LastName_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "LastName", "ASC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "LastName", "ASC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "LastName", "ASC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(7));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(5));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(7));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(2));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(6));

        }

        [Test]
        public void GetAll_When_sorting_by_LastName_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "LastName", "DESC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "LastName", "DESC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "LastName", "DESC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(7));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(3));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(6));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(3));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(8));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(5));

        }

        [Test]
        public void GetAll_When_sorting_by_DateOfBirth_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "DateOfBirth", "ASC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "DateOfBirth", "ASC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "DateOfBirth", "ASC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(110));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(4));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(5));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(9));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(4));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(1));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(9));

        }

        [Test]
        public void GetAll_When_sorting_by_DateOfBirth_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new AuthorService(_fakeUnitOfWorkFactory, _mapper);

            // Act
            var authors1 = service.GetAll(null, null, "DateOfBirth", "DESC", 0, pageNumb: 1, pageCount: 10);
            var authors2 = service.GetAll(null, null, "DateOfBirth", "DESC", 0, pageNumb: 2, pageCount: 5);
            var authors3 = service.GetAll(null, null, "DateOfBirth", "DESC", 0, pageNumb: 3, pageCount: 2);


            // Assert
            Assert.That(authors1.ToList()[0].Id, Is.EqualTo(4));
            Assert.That(authors1.ToList()[9].Id, Is.EqualTo(110));
            Assert.That(authors1.ToList()[4].Id, Is.EqualTo(9));

            Assert.That(authors2.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(authors2.ToList()[4].Id, Is.EqualTo(110));
            Assert.That(authors2.ToList()[2].Id, Is.EqualTo(7));

            Assert.That(authors3.ToList()[0].Id, Is.EqualTo(9));
            Assert.That(authors3.ToList()[1].Id, Is.EqualTo(5));

        }

    }
}
