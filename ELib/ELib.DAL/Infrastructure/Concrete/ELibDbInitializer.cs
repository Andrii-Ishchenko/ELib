using System.Collections.Generic;
using System.Linq;
using ELib.Domain.Entities;
using System.Data.Entity;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ELib.DAL.Infrastructure.Concrete
{
    public class ELibDbInitializer : CreateDatabaseIfNotExists<ELibDbContext>
    {
        protected override void Seed(ELibDbContext context)
        {

            //users
            var user1 = new ApplicationUser() { Id = "98f9156c-f396-4c25-9cdb-8f559da4046c", Email = "John@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "John" };
            var user2 = new ApplicationUser() { Id = "f43b6e5c-c4e5-4db5-9d7b-813f77edcaec", Email = "Frank@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Frank" };
            var user3 = new ApplicationUser() { Id = "98f9156c-4c25-f396-9cdb-8f559da4047c", Email = "Eva@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Eva" };
            var user4 = new ApplicationUser() { Id = "98f9156c-c4e5-4c25-9cdb-8f559da4048c", Email = "Peter@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Peter" };
            var user5 = new ApplicationUser() { Id = "98f9156c-9cdb-f396-4c25-8f559da4049c", Email = "Howard@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Howard" };
            var user6 = new ApplicationUser() { Id = "98f9156c-f396-9cdb-9cdb-8f559da4050c", Email = "Mary@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Mary" };
            var user7 = new ApplicationUser() { Id = "98f9156c-f396-4c25-9cdb-8f559da4051c", Email = "Simón@mail.ru", EmailConfirmed = false, PasswordHash = "AFO3zX6UgpLp5+2KG6CHTCGxBA6GxYPA30Q4dejAe8P4tf/p4vcPHcKi1jVwkhsNhw==", SecurityStamp = "8e83a385-aa50-4055-86dc-0260ed971a20", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, UserName = "Simón" };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);
            context.Users.Add(user6);
            context.Users.Add(user7);
            context.SaveChanges();

            //users-roles
            var userRole1 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4046c", UserId = "98f9156c-f396-4c25-9cdb-8f559da4046c" };
            var userRole2 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4046c", UserId = "f43b6e5c-c4e5-4db5-9d7b-813f77edcaec" };
            var userRole3 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4047c", UserId = "98f9156c-4c25-f396-9cdb-8f559da4047c" };
            var userRole4 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4047c", UserId = "98f9156c-c4e5-4c25-9cdb-8f559da4048c" };
            var userRole5 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4048c", UserId = "98f9156c-9cdb-f396-4c25-8f559da4049c" };
            var userRole6 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4048c", UserId = "98f9156c-f396-9cdb-9cdb-8f559da4050c" };
            var userRole7 = new IdentityUserRole() { RoleId = "98f9156c-f396-4c25-9cdb-8f559da4048c", UserId = "98f9156c-f396-4c25-9cdb-8f559da4051c" };

            //roles

            context.Roles.Add(new IdentityRole() { Id = "98f9156c-f396-4c25-9cdb-8f559da4046c", Name = "ApprovedMember", Users = { userRole1, userRole2 } });
            context.Roles.Add(new IdentityRole() { Id = "98f9156c-f396-4c25-9cdb-8f559da4047c", Name = "Moderator", Users = { userRole3, userRole4 } });
            context.Roles.Add(new IdentityRole() { Id = "98f9156c-f396-4c25-9cdb-8f559da4048c", Name = "Administrator", Users = { userRole5, userRole6, userRole7 } });
            context.SaveChanges();

            //people
            var people = new List<Person>();
            people.Add(new Person() { FirstName = "John", Login = "John", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-f396-4c25-9cdb-8f559da4046c" });
            people.Add(new Person() { FirstName = "Frank", Login = "Frank", RegistrationDate = DateTime.Now, AplicationUserId = "f43b6e5c-c4e5-4db5-9d7b-813f77edcaec" });
            people.Add(new Person() { FirstName = "Eva", Login = "Eva", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-4c25-f396-9cdb-8f559da4047c" });
            people.Add(new Person() { FirstName = "Peter", Login = "Peter", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-c4e5-4c25-9cdb-8f559da4048c" });
            people.Add(new Person() { FirstName = "Howard", Login = "Howard", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-9cdb-f396-4c25-8f559da4049c" });
            people.Add(new Person() { FirstName = "Mary", Login = "Mary", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-f396-9cdb-9cdb-8f559da4050c" });
            people.Add(new Person() { FirstName = "Simón", Login = "Simón", RegistrationDate = DateTime.Now, AplicationUserId = "98f9156c-f396-4c25-9cdb-8f559da4051c" });
            context.People.AddRange(people);
            context.SaveChanges();





            //genres
            var genres = new List<Genre>();
            genres.Add(new Genre() { Name = "Техническая литература" });
            genres.Add(new Genre() { Name = "Детектив" });
            genres.Add(new Genre() { Name = "Ужасы" });
            genres.Add(new Genre() { Name = "Женский роман" });
            genres.Add(new Genre() { Name = "Фантастика" });
            genres.Add(new Genre() { Name = "Фэнтези" });
            genres.Add(new Genre() { Name = "Драма" });
            context.Genres.AddRange(genres);

            //subgenres
            var subgenres = new List<Subgenre>();
            subgenres.Add(new Subgenre() { Name = "Роман" });
            context.Subgenres.AddRange(subgenres);


            //authors
            var authors = new List<Author>();
            authors.Add(new Author()
            {
                FirstName = "Джефри",
                LastName = "Рихтер",
                Description = " Компьютерный специалист, автор наиболее хорошо продаваемых книг в области Win32 и .NET." +
                " Рихтер — соучредитель компании Wintellect, которая обучает ИТ-специалистов и консультирует фирмы в области создания ПО."
            });
            authors.Add(new Author()
            {
                FirstName = "Стивен",
                LastName = "Кинг",
                Description = " Американский писатель, работающий в разнообразных жанрах, включая ужасы, триллер, фантастику," +
                " фэнтези, мистику, драму; получил прозвище «Король ужасов». Продано более 350 миллионов экземпляров его книг," +
                " по которым был снят ряд художественных фильмов, телевизионных постановок, а также нарисованы комиксы.",
                DateOfBirth = new DateTime(1947, 9, 21)
            });
            authors.Add(new Author()
            {
                FirstName = "Эрих",
                LastName = "Гамма",
                Description = "Программист из Швейцарии, один из четырёх авторов классической книги Design Patterns о шаблонах проектирования." +
                " Команда авторов книги также известна под названием «банда четырёх» (англ. Gang of Four, GoF). Является ведущим разработчиком JUnit " +
                "и Eclipse (кросс-платформенной интегрированной среды разработки программного обеспечения). Работал в IBM над проектом Jazz. " +
                " С 2011 года руководит командой разработки Microsoft Visual Studio в Цюрихе, Швейцария.",
                DateOfBirth = new DateTime(1961, 3, 13)
            });
            authors.Add(new Author() { FirstName = "Ричард", LastName = "Хелм", });
            authors.Add(new Author() { FirstName = "Ральф", LastName = "Джонсон", Description = "", DateOfBirth = new DateTime(1955, 10, 7) });
            authors.Add(new Author() { FirstName = "Джон", LastName = "Влиссидс", Description = "", DateOfBirth = new DateTime(1961, 8, 2), DeathDate = new DateTime(2005, 11, 24) });
            context.Authors.AddRange(authors);
            context.SaveChanges();

            //authGenres
            var authGenres = new List<AuthorGenre>();
            authGenres.Add(new AuthorGenre() { AuthorId = 1, GenreId = 1 });
            authGenres.Add(new AuthorGenre() { AuthorId = 2, GenreId = 3 });
            authGenres.Add(new AuthorGenre() { AuthorId = 2, GenreId = 5 });
            authGenres.Add(new AuthorGenre() { AuthorId = 2, GenreId = 6 });
            authGenres.Add(new AuthorGenre() { AuthorId = 2, GenreId = 7 });
            authGenres.Add(new AuthorGenre() { AuthorId = 3, GenreId = 1 });
            authGenres.Add(new AuthorGenre() { AuthorId = 4, GenreId = 1 });
            authGenres.Add(new AuthorGenre() { AuthorId = 5, GenreId = 1 });
            authGenres.Add(new AuthorGenre() { AuthorId = 6, GenreId = 1 });
            context.AuthorGenres.AddRange(authGenres);

            //publishers
            context.Publishers.Add(new Publisher { Name = "Аст" });
            context.Publishers.Add(new Publisher { Name = "Астрель" });
            context.Publishers.Add(new Publisher { Name = "Питер" });
            context.Publishers.Add(new Publisher { Name = "Microsoft Press" });

            //languages
            context.Languages.Add(new Language { Name = "RU" });
            context.Languages.Add(new Language { Name = "UK" });
            context.Languages.Add(new Language { Name = "EN" });
            context.Languages.Add(new Language { Name = "IT" });
            context.Languages.Add(new Language { Name = "FR" });
            context.Languages.Add(new Language { Name = "DE" });
            context.Languages.Add(new Language { Name = "PR" });
            context.SaveChanges();

            //books
            var books = new List<Book>();
            books.Add(new Book()
            {
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
                SubgenreId = 1,
                Isbn = "978-5-496-00433-6",
                PublishYear = new DateTime(2013, 1, 1),
                TotalPages = 896
            });
            books.Add(new Book()
            {
                Title = "CLR via C#",
                Description = "",
                PublishLangId = 3,
                Isbn = "978-0-7356-6745-7",
                PublishYear = new DateTime(2012, 1, 1),
                SubgenreId = 1,
                TotalPages = 870,
                PublisherId = 4
            });
            books.Add(new Book()
            {
                Title = "Оно",
                Description = "В маленьком провинциальном городке Дерри много лет назад семерым подросткам пришлось столкнуться с кромешным ужасом" +
                              " – живым воплощением ада. Прошли годы… Подростки повзрослели, и ничто, казалось, не предвещало новой беды. Но кошмар " +
                              "прошлого вернулся, неведомая сила повлекла семерых друзей назад, в новую битву со Злом. Ибо в Дерри опять льется кровь" +
                              " и бесследно исчезают люди. Ибо вернулось порождение ночного кошмара, настолько невероятное, что даже не имеет имени… ",
                OriginalLangId = 3,
                PublishLangId = 1,
                SubgenreId = 1,
                PublisherId = 1,
                Isbn = "978-5-17-077763-1",
                PublishYear = new DateTime(2015, 8, 3),
                TotalPages = 1248
            });
            books.Add(new Book()
            {
                Title = "Кэрри",
                Description = "Маленький провинциальный городок в Новой Англии в одночасье становится \"мертвым городом \". На улицах лежат трупы, " +
                "над домами бушует смертоносное пламя. И весь этот кошмар огненного Апокалипсиса ― дело рук одного человека, девушки Кэрри, жалкой, " +
                "запуганной дочери чудаковатой вдовы. Долгие годы дремал в Кэрри талант телекинеза, чтобы однажды проснуться. И тогда в городок пришла смерть...",
                OriginalLangId = 3,
                PublishLangId = 1,
                SubgenreId = 1,
                PublisherId = 2,
                Isbn = "978-5-17-078099-0",
                PublishYear = new DateTime(2013, 1, 1),
                TotalPages = 320
            });
            books.Add(new Book()
            {
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
                PublisherId = 3,
                Isbn = "5-272-00355-1",
                PublishYear = new DateTime(2001, 1, 1),
                TotalPages = 352
            });
            context.Books.AddRange(books);
            context.SaveChanges();

            //bookGenres
            context.BookGenres.Add(new BookGenre { BookId = 1, GenreId = 1 });
            context.BookGenres.Add(new BookGenre { BookId = 2, GenreId = 1 });
            context.BookGenres.Add(new BookGenre { BookId = 3, GenreId = 2 });
            context.BookGenres.Add(new BookGenre { BookId = 4, GenreId = 2 });
            context.BookGenres.Add(new BookGenre { BookId = 5, GenreId = 1 });

            //bookAuthors
            context.BookAuthors.Add(new BookAuthor { AuthorId = 1, BookId = 1 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 1, BookId = 2 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 2, BookId = 3 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 2, BookId = 4 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 3, BookId = 5 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 4, BookId = 5 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 5, BookId = 5 });
            context.BookAuthors.Add(new BookAuthor { AuthorId = 6, BookId = 5 });


            context.SaveChanges();
            base.Seed(context);
        }
    }
}
