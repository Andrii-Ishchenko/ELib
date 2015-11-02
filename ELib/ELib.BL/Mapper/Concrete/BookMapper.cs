using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;
using System.Linq;
using System.Collections.Generic;

namespace ELib.BL.Mapper.Concrete
{
    public class BookMapper : IMapper<Book, BookDto>
    {
        IMapper<BookInstance, BookInstanceDto> _bookInstanceMapper;
        IMapper<Author, AuthorDto> _authorMapper;


        public BookMapper(IMapper<BookInstance, BookInstanceDto> bookInstanceMapper, IMapper<Author, AuthorDto> authorMapper)
        {
            _bookInstanceMapper = bookInstanceMapper;
            _authorMapper = authorMapper;

        }

        public Book Map(BookDto input)
        {
            Book result = new Book()
            {
                Id = input.Id,
                Title = input.Title,
                AdditionDate = input.AdditionDate,
                CategoryId = input.CategoryId,
                Description = input.Description,
                PublisherId = input.PublisherId,
                PublishLangId = input.PublishLangId,
                PublishYear = input.PublishYear,
                SumRatingValue = input.Rating,
                ImageHash = input.ImageHash,
                Isbn = input.Isbn,
                TotalPages = input.TotalPages,
                OriginalLangId = input.OriginalLangId,
                TotalDownloadCount = input.TotalDownloadCount,
                SubgenreId = input.SubgenreId,
                TotalViewCount = input.TotalViewCount
            };
            result.BookAuthors = (input.Authors == null) ? null : input.Authors.Select(a => new BookAuthor {Id = a.BookAuthorsId, AuthorId = a.Id, BookId = input.Id }).ToList();
            result.BookGenres = (input.Genres == null) ? null : input.Genres.Select(g => new BookGenre {Id = g.BookGenreId, GenreId = g.GenreId, BookId = input.Id }).ToList();
            //if (input.Authors != null)
            //    foreach (var author in input.Authors)
            //        result.BookAuthors.Add(new BookAuthor()
            //        {
            //            AuthorId = author.Id,
            //            BookId = input.Id
            //            //Author=new Author() {Id = author.Id, FirstName = author.FirstName, LastName = author.LastName } });
            //        });

            return result;
        }

        public BookDto Map(Book input)
        {
            BookDto result = new BookDto()
            {
                Id = input.Id,
                Title = input.Title,
                AdditionDate = input.AdditionDate,
                CategoryId = input.CategoryId,
                Description = input.Description,
                PublisherId = input.PublisherId,
                PublishLangId = input.PublishLangId,
                PublishYear = input.PublishYear,
                Rating = input.SumRatingValue,
                ImageHash = input.ImageHash,
                Isbn = input.Isbn,
                TotalPages = input.TotalPages,
                OriginalLangId = input.OriginalLangId,
                TotalDownloadCount = input.TotalDownloadCount,
                SubgenreId = input.SubgenreId,
                TotalViewCount = input.TotalViewCount,
                CategoryName = (input.Category == null) ? null : input.Category.Name,
                Language1Name = (input.Language1 == null) ? null : input.Language1.Name,
                LanguageName = (input.Language == null) ? null : input.Language.Name,
                PublisherName = (input.Publisher == null) ? null : input.Publisher.Name,
                SubgenreName = (input.Subgenre == null) ? null : input.Subgenre.Name
            };
            result.Genres = input.BookGenres.Select(g => new GenreForBookDto { GenreId = g.GenreId, BookGenreId = g.Id, GenreName = (g.Genre == null) ? null : g.Genre.Name }).ToList();
            result.BookInstances = input.BookInstances.Select(bi => _bookInstanceMapper.Map(bi)).ToList();
            // result.Authors = input.BookAuthors.Select(ba => _authorMapper.Map(ba.Author)).ToList();
            List<AuthorForBookDto> authors = new List<AuthorForBookDto>();
            if (input.BookAuthors != null)
                foreach (var author in input.BookAuthors)
                    if (author.Author != null)
                    {
                        authors.Add(new AuthorForBookDto()
                        {
                            Id = author.AuthorId,
                            BookAuthorsId = author.Id,
                            FirstName = author.Author.FirstName,
                            LastName = author.Author.LastName
                        });
                    }
                    else
                    {
                        authors.Add(new AuthorForBookDto()
                        {
                            BookAuthorsId = author.Id,
                            Id = author.AuthorId
                        });
                    }
            result.Authors = authors;
            return result;
        }
    }
}
