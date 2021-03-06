﻿using System;
using System.Collections.Generic;
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
    public class BookInListServiceTests
    {
        private readonly FakeUnitOfWorkFactory _fakeUnitOfWorkFactory;
        private readonly FakeRepository<Book> _fakeBookRepository;
        private readonly FakeRepository<Author> _fakeAuthorRepository;
        private readonly FakeRepository<Publisher> _fakePublisherRepository;
        private readonly FakeRepository<BookAuthor> _fakeBookAuthorRepository;
        private readonly IMapper<Author, AuthorDto> _mapperAuthor;
        private readonly IMapper<Book, BookInListDto> _mapperBook;

        public BookInListServiceTests()
        {
            _mapperAuthor = new AuthorMapper();
            _mapperBook = new BookInListMapper(_mapperAuthor);

            _fakePublisherRepository = new FakeRepository<Publisher>();
            _fakeAuthorRepository = new FakeRepository<Author>();
            _fakeBookRepository = new FakeRepository<Book>();
            _fakeBookAuthorRepository = new FakeRepository<BookAuthor>();

            _fakeUnitOfWorkFactory = new FakeUnitOfWorkFactory(
                uow =>
                {
                    uow.SetRepository(_fakePublisherRepository);
                    uow.SetRepository(_fakeAuthorRepository);
                    uow.SetRepository(_fakeBookRepository);
                    uow.SetRepository(_fakeBookAuthorRepository);
                });
        }

        [SetUp]
        public void TestSetup()
        {
            #region publishers
            var publisher1 = new Publisher { Id = 1, Name = "Аверс" };
            var publisher2 = new Publisher { Id = 2, Name = "Oma-Book" };
            var publisher3 = new Publisher { Id = 3, Name = "Фактор" };
            var publisher4 = new Publisher { Id = 4, Name = "Просвита" };
            var publisher5 = new Publisher { Id = 5, Name = "FriedensBote" };
            var publisher6 = new Publisher { Id = 6, Name = "АФІША" };
            var publisher7 = new Publisher { Id = 7, Name = "Диалектика" };
            var publisher8 = new Publisher { Id = 8, Name = "41Аверс" };
            #endregion

            #region authors
            var author1 = new Author { Id = 1, FirstName = "Джефри", LastName = "Иванов", DateOfBirth = new DateTime(1990, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author2 = new Author { Id = 2, FirstName = "3456", LastName = "Аметов", DateOfBirth = new DateTime(1990, 7, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author3 = new Author { Id = 3, FirstName = "Nick", LastName = "12345", DateOfBirth = new DateTime(1990, 5, 29), DeathDate = new DateTime(2011, 6, 1) };
            var author4 = new Author { Id = 4, FirstName = "Дмитрий", LastName = "3Степаненко", DateOfBirth = new DateTime(1991, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author5 = new Author { Id = 5, FirstName = "Daniel", LastName = "Petrov", DateOfBirth = new DateTime(1790, 4, 1), DeathDate = new DateTime(2015, 6, 1) };
            var author6 = new Author { Id = 6, FirstName = "Daniel1", LastName = "Petrov2", DateOfBirth = new DateTime(1690, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            var author7 = new Author { Id = 7, FirstName = "5Николай", LastName = "Петренко", DateOfBirth = new DateTime(1537, 11, 5), DeathDate = new DateTime(2008, 6, 1) };
            var author8 = new Author { Id = 8, FirstName = "Иван", LastName = "6Рихтер", DateOfBirth = new DateTime(1450, 6, 1), DeathDate = new DateTime(2008, 6, 1) };
            #endregion

            #region bookAuthor
            var bookAuthor1 = new BookAuthor { Id = 1, BookId = 1, AuthorId = 2, Author = author2 };
            var bookAuthor2 = new BookAuthor { Id = 2, BookId = 2, AuthorId = 4, Author = author4 };
            var bookAuthor3 = new BookAuthor { Id = 3, BookId = 2, AuthorId = 5, Author = author5 };
            var bookAuthor4 = new BookAuthor { Id = 4, BookId = 3, AuthorId = 3, Author = author3 };
            var bookAuthor5 = new BookAuthor { Id = 5, BookId = 4, AuthorId = 1, Author = author1 };
            var bookAuthor6 = new BookAuthor { Id = 6, BookId = 5, AuthorId = 8, Author = author8 };
            var bookAuthor7 = new BookAuthor { Id = 7, BookId = 6, AuthorId = 1, Author = author1 };
            var bookAuthor8 = new BookAuthor { Id = 8, BookId = 6, AuthorId = 2, Author = author2 };
            var bookAuthor9 = new BookAuthor { Id = 9, BookId = 7, AuthorId = 2, Author = author2 };
            var bookAuthor10 = new BookAuthor { Id = 10, BookId = 8, AuthorId = 6, Author = author6 };
            var bookAuthor11 = new BookAuthor { Id = 11, BookId = 8, AuthorId = 7, Author = author7 };
            var bookAuthor12 = new BookAuthor { Id = 12, BookId = 8, AuthorId = 8, Author = author8 };
            var bookAuthor13 = new BookAuthor { Id = 13, BookId = 9, AuthorId = 4, Author = author4 };
            var bookAuthor14 = new BookAuthor { Id = 14, BookId = 10, AuthorId = 3, Author = author3 };
            var bookAuthor15 = new BookAuthor { Id = 15, BookId = 10, AuthorId = 8, Author = author8 };
            #endregion

            #region books
            var books = new Book[]
            {
                new Book()
                {
                    Id=1,
                    Title = "Программирование на платформе Microsoft .NET Framework 4.5 на языке C#",
                    Description = "Эта книга, выходящая в четвертом издании и уже ставшая классическим учебником по" +
                    "программированию, подробно описывает внутреннее устройство и функционирование общеязыковой исполняющей " +
                    " среды (CLR) Microsoft .NET Framework версии 4.5. Написанная признанным экспертом в области " +
                    "программирования Джеффри Рихтером, много лет являющимся консультантом команды разработчиков .NET " +
                    " Framework компании Microsoft, книга научит вас создавать по-настоящему надежные приложения любого вида, " +
                    "в том числе с использованием Microsoft Silverlight, ASP.NET, Windows Presentation Foundation и т. д.",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    PublisherId = 3,
                    Publisher=publisher3,
                    SubgenreId = 1,
                    Isbn = "978-5-496-00433-6",
                    PublishYear = 2013,
                    TotalPages = 896,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2013, 1, 1),
                    SumRatingValue=88,
                    BookAuthors=new List<BookAuthor> { bookAuthor1 }
                },
                new Book()
                {
                    Id=2,
                    Title = "CLR via C#",
                    Description = "",
                    PublishLangId = 3,
                    Isbn = "978-0-7356-6745-7",
                    PublishYear = 2012,
                    SubgenreId = 1,
                    TotalPages = 870,
                    PublisherId = 4,
                    Publisher=publisher4,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2012, 1, 1),
                    SumRatingValue=2,
                    BookAuthors=new List<BookAuthor> { bookAuthor2,bookAuthor3 }
                },
                new Book()
                {
                    Id=3,
                    Title = "Оно",
                    Description = "В маленьком провинциальном городке Дерри много лет назад семерым подросткам пришлось столкнуться с кромешным ужасом" +
                                  " – живым воплощением ада. Прошли годы… Подростки повзрослели, и ничто, казалось, не предвещало новой беды. Но кошмар " +
                                  "прошлого вернулся, неведомая сила повлекла семерых друзей назад, в новую битву со Злом. Ибо в Дерри опять льется кровь" +
                                  " и бесследно исчезают люди. Ибо вернулось порождение ночного кошмара, настолько невероятное, что даже не имеет имени… ",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 1,
                    Publisher=publisher1,
                    Isbn = "978-5-17-077763-1",
                    PublishYear = 2015,
                    TotalPages = 1248,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2015, 8, 3),
                    SumRatingValue=100,
                    BookAuthors=new List<BookAuthor> { bookAuthor4 }
                },
                new Book()
                {
                    Id=4,
                    Title = "Кэрри",
                    Description = "Маленький провинциальный городок в Новой Англии в одночасье становится \"мертвым городом \". На улицах лежат трупы, " +
                    "над домами бушует смертоносное пламя. И весь этот кошмар огненного Апокалипсиса ― дело рук одного человека, девушки Кэрри, жалкой, " +
                    "запуганной дочери чудаковатой вдовы. Долгие годы дремал в Кэрри талант телекинеза, чтобы однажды проснуться. И тогда в городок пришла смерть...",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 2,
                    Publisher=publisher2,
                    Isbn = "978-5-17-078099-0",
                    PublishYear = 1999,
                    TotalPages = 320,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2013, 1, 1),
                    SumRatingValue=34,
                    BookAuthors=new List<BookAuthor> { bookAuthor5 }
                },
                new Book()
                {
                    Id=5,
                    Title = "Приемы объектно-ориентированого програмирования. Паттерны проэктирования",
                    Description = "В предлагаемой книге описываются простые и изящные решения типичных задач, возникающих в " +
                                  "объектно-ориентированном проектировании. Паттерны появились потому, что многие разработчики" +
                                  " искали пути повышения гибкости и степени повторного использования своих программ. Найденные решения" +
                                  "воплощены в краткой и легко применимой на практике форме. Авторы излагают принципы использования" +
                                  "паттернов проектирования и приводят их каталог. Таким образом, книга одновременно решает две задачи." +
                                  "Во-первых, здесь демонстрируется роль паттернов в создании архитектуры сложных систем. Во-вторых, " +
                                   "применяя содержащиеся в справочнике паттерны, проектировщик сможет с легкостью разрабатывать собственные приложения.",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 5,
                    Publisher=publisher5,
                    Isbn = "5-272-00355-1",
                    PublishYear = 2001,
                    TotalPages = 352,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2012, 1, 1),
                    SumRatingValue=99,
                    BookAuthors=new List<BookAuthor> { bookAuthor6 }
                },
                 new Book()
                {
                    Id=6,
                    Title = "Выразительный Javascript",
                    Description = "",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 8,
                    Publisher=publisher8,
                    Isbn = "978-5-17-0134564-1",
                    PublishYear = 1999,
                    TotalPages = 1348,
                    CategoryId = 3,
                    AdditionDate = new DateTime(2015, 6, 30),
                    SumRatingValue=79,
                    BookAuthors=new List<BookAuthor> { bookAuthor7,bookAuthor8 }
                },
                  new Book()
                {
                    Id=7,
                    Title = "Javascript Enlightenment",
                    Description = "",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 6,
                    Publisher=publisher6,
                    Isbn = "988-5-17-077763-1",
                    PublishYear = 2013,
                    TotalPages = 1248,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2012, 1, 11),
                    SumRatingValue=12,
                    BookAuthors= new List<BookAuthor> {bookAuthor9},

                },
                   new Book()
                {
                    Id=8,
                    Title = "Mastering NodeJS",
                    Description = "",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 7,
                    Publisher=publisher7,
                    Isbn = "958-5-17-077763-1",
                    PublishYear = 2010,
                    TotalPages = 1248,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2010, 12, 31),
                    SumRatingValue=34,
                    BookAuthors=new List<BookAuthor> { bookAuthor10,bookAuthor11,bookAuthor12 }
                },
                    new Book()
                {
                    Id=9,
                    Title = "Smooth CoffeeScript",
                    Description = "",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 2,
                    Publisher=publisher2,
                    Isbn = "978-5-17-077763-5",
                    PublishYear = 2015,
                    TotalPages = 1248,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2015, 5, 3),
                    SumRatingValue=88,
                    BookAuthors=new List<BookAuthor> { bookAuthor13 }
                },
                     new Book()
                {
                    Id=10,
                    Title = "Node Beginner",
                    Description = "",
                    OriginalLangId = 3,
                    PublishLangId = 1,
                    SubgenreId = 1,
                    PublisherId = 4,
                    Publisher=publisher4,
                    Isbn = "978-5-17-077763-1",
                    PublishYear = 2013,
                    TotalPages = 148,
                    CategoryId = 1,
                    AdditionDate = new DateTime(2015, 8, 29),
                    SumRatingValue=79,
                    BookAuthors=new List<BookAuthor> { bookAuthor14,bookAuthor15 }
                },
            };
            #endregion
            
            _fakeAuthorRepository.Data.AddRange(new[] { author1, author2, author3, author4, author5, author6, author7, author8 });
            _fakePublisherRepository.Data.AddRange(new[] { publisher1, publisher2, publisher3, publisher4, publisher5, publisher6, publisher7, publisher8 });
            _fakeBookRepository.Data.AddRange(books);
            _fakeBookAuthorRepository.Data.AddRange(new[] { bookAuthor1, bookAuthor2, bookAuthor3, bookAuthor4, bookAuthor5, bookAuthor6, bookAuthor7, bookAuthor8, bookAuthor9,  bookAuthor10, bookAuthor11, bookAuthor12, bookAuthor13, bookAuthor14, bookAuthor15});
        }

        // This method runs after each test.
        [TearDown]
        public void TestDown()
        {
            // Clean up data in our fake dependencies.
            _fakeBookAuthorRepository.Data.Clear();
            _fakeBookRepository.Data.Clear();
            _fakePublisherRepository.Data.Clear();
            _fakeAuthorRepository.Data.Clear();
        }

        [Test]
        public void GetAll_When_sorting_by_Title_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Title", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(2));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(1));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(9));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(1));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(3));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(9));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(6));
        }

        [Test]
        public void GetAll_When_sorting_by_Title_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Title", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(2));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(6));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(9));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(2));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(8));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(9));
        }

        [Test]
        public void GetAll_When_sorting_by_Year_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Year", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(4));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(9));
            Assert.That(books1.ToList()[2].Id, Is.EqualTo(5));
            Assert.That(books1.ToList()[6].Id, Is.EqualTo(7));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(9));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(10));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(2));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(1));
        }

        [Test]
        public void GetAll_When_sorting_by_Year_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Year", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(9));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(4));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(1));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(2));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(4));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(5));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(2));
        }

        [Test]
        public void GetAll_When_sorting_by_Rating_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Rating", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(2));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(3));
            Assert.That(books1.ToList()[2].Id, Is.EqualTo(4));
            Assert.That(books1.ToList()[6].Id, Is.EqualTo(1));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(10));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(3));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(9));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(10));
        }

        [Test]
        public void GetAll_When_sorting_by_Rating_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Rating", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(2));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(10));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(2));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(4));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(10));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(6));
        }

        [Test]
        public void GetAll_When_sorting_by_Date_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Date", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(8));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(10));
            Assert.That(books1.ToList()[2].Id, Is.EqualTo(5));
            Assert.That(books1.ToList()[6].Id, Is.EqualTo(9));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(4));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(10));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(6));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(4));
        }

        [Test]
        public void GetAll_When_sorting_by_Date_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Date", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(10));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(8));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(4));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(8));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(5));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(4));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(1));
        }

        [Test]
        public void GetAll_When_sorting_by_Publisher_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Publisher", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(1));
            Assert.That(books1.ToList()[2].Id, Is.EqualTo(4));
            Assert.That(books1.ToList()[6].Id, Is.EqualTo(8));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(7));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(1));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(2));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(7));
        }

        [Test]
        public void GetAll_When_sorting_by_Publisher_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Publisher", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(1));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(6));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(7));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(6));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(4));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(7));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(3));
        }

        [Test]
        public void GetAll_When_sorting_by_Author_ASC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Author", orderDirection: "ASC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(3));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(6));
            Assert.That(books1.ToList()[2].Id, Is.EqualTo(2));
            Assert.That(books1.ToList()[6].Id, Is.EqualTo(1));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(8));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(6));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(7));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(8));
        }

        [Test]
        public void GetAll_When_sorting_by_Author_DESC_Then_get_sorted_list()
        {
            // Arrange
            var service = new BookInListService(_fakeUnitOfWorkFactory, _mapperBook);

            // Act
            SearchDto serch = new SearchDto(query: null, authorName: null, title: null, publisher: null, genre: null, subgenre: null, genreId: -1, subgenreId: -1, year: -1, categoryIds: null, orderby: "Author", orderDirection: "DESC");

            var books1 = service.GetAll(searchDto: serch, pageNumb: 1, pageCount: 10);
            var books2 = service.GetAll(searchDto: serch, pageNumb: 2, pageCount: 5);
            var books3 = service.GetAll(searchDto: serch, pageNumb: 3, pageCount: 2);

            // Assert
            Assert.That(books1.ToList()[0].Id, Is.EqualTo(6));
            Assert.That(books1.ToList()[9].Id, Is.EqualTo(3));
            Assert.That(books1.ToList()[4].Id, Is.EqualTo(8));

            Assert.That(books2.ToList()[0].Id, Is.EqualTo(5));
            Assert.That(books2.ToList()[4].Id, Is.EqualTo(3));
            Assert.That(books2.ToList()[2].Id, Is.EqualTo(2));

            Assert.That(books3.ToList()[0].Id, Is.EqualTo(8));
            Assert.That(books3.ToList()[1].Id, Is.EqualTo(5));
        }
    }
}
