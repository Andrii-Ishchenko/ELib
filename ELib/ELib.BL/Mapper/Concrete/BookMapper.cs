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
        IMapper<Publisher, PublisherDto> _publisherMapper;
        IMapper<Language, LanguageDto> _languageMapper;
        IMapper<Subgenre, SubgenreDto> _subgenreMapper;
        IMapper<Category, CategoryDto> _categoryMapper;


        public BookMapper(IMapper<BookInstance, BookInstanceDto> bookInstanceMapper,
                          IMapper<Author, AuthorDto> authorMapper,
                          IMapper<Publisher, PublisherDto> publisherMapper,
                          IMapper<Language, LanguageDto> languageMapper,
                          IMapper<Subgenre, SubgenreDto> subgenreMapper,
                          IMapper<Category, CategoryDto> categoryMapper)
        {
            _bookInstanceMapper = bookInstanceMapper;
            _authorMapper = authorMapper;
            _publisherMapper = publisherMapper;
            _languageMapper = languageMapper;
            _subgenreMapper = subgenreMapper;
            _categoryMapper = categoryMapper;
        }

        public Book Map(BookDto input)
        {
            if (input == null)
                return null;
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
                TotalViewCount = input.TotalViewCount,
                State = input.State
            };
            result.Category = _categoryMapper.Map(input.Category);
            result.Language = _languageMapper.Map(input.Language);
            result.Language1 = (result.PublishLangId == result.OriginalLangId) ? result.Language : _languageMapper.Map(input.Language1);
            result.Publisher = _publisherMapper.Map(input.Publisher);
            result.Subgenre = _subgenreMapper.Map(input.Subgenre);
            result.BookAuthors = (input.Authors == null) ? result.BookAuthors : input.Authors.Select(a => new BookAuthor {Id = a.BookAuthorsId, AuthorId = a.Id, BookId = input.Id, State = a.State }).ToList();
            result.BookGenres = (input.Genres == null) ? result.BookGenres : input.Genres.Select(g => new BookGenre {Id = g.BookGenreId, GenreId = g.GenreId, BookId = input.Id, State = g.State}).ToList();
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
            if (input == null)
                return null;
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
                State = input.State,
                Category = _categoryMapper.Map(input.Category),
                Language1 = _languageMapper.Map(input.Language1),
                Language = _languageMapper.Map(input.Language),
                Publisher = _publisherMapper.Map(input.Publisher),
                Subgenre = _subgenreMapper.Map(input.Subgenre)
            };
            result.Genres = input.BookGenres.Select(g => new GenreForBookDto { GenreId = g.GenreId, BookGenreId = g.Id, GenreName = (g.Genre == null) ? null : g.Genre.Name, State = g.State }).ToList();
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
                            LastName = author.Author.LastName,

                            //AuthorForBook State now equals to BookAuthor Pair State
                            State = author.State
                        });
                    }
                    else
                    {
                        authors.Add(new AuthorForBookDto()
                        {
                            BookAuthorsId = author.Id,
                            Id = author.AuthorId,

                            //AuthorForBook State now equals to BookAuthor Pair State
                            State = author.State
                        });
                    }
            result.Authors = authors;
            return result;
        }
    }
}
