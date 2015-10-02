﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Mapper.Concrete
{
    public class BookMapper : IMapper<Book, BookDto>
    {
        IMapper<BookInstance, BookInstanceDto> _bookInstanceMapper;
        IMapper<Author, AuthorListDto> _authorMapper;

        public BookMapper(IMapper<BookInstance, BookInstanceDto>bookInstanceMapper, IMapper<Author, AuthorListDto>authorMapper)
        {
            _bookInstanceMapper = bookInstanceMapper;
            _authorMapper = authorMapper;
        }

        public Book Map(BookDto input)
        {
            Book result = new Book() // ? решта полів нулі?
            {
                Id = input.Id,
                Title = input.Title,
                AdditionDate = input.AdditionDate,
                CategoryId = input.CategoryId,
                Description = input.Description,
                PublisherId = input.PublisherId,
                PublishLangId = input.PublishLangId,
                PublishYear = input.PublishYear,
                SumRatingValue = input.SumRatingValue,
                ImageHash = input.ImageHash,
                Isbn = input.Isbn,
                TotalPages = input.TotalPages,
                OriginalLangId = input.OriginalLangId,
                TotalDownloadCount = input.TotalDownloadCount,
                SubgenreId = input.SubgenreId,
                TotalViewCount = input.TotalViewCount
            };

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
                CategoryName = input.Category.Name,
                Language1Name = input.Language1.Name,
                LanguageName = input.Language.Name,
                PublisherName = input.Publisher.Name,
                SubgenreName = input.Subgenre.Name
            };

            result.Authors = input.BookAuthors.Select(a => a.Author.FirstName + " " + a.Author.LastName).ToList();
            result.AuthorsIds = input.BookAuthors.Select(a => a.AuthorId).ToList();
            result.GenresNames = input.BookGenres.Select(g => g.Genre.Name).ToList();
            result.GenresIds = input.BookGenres.Select(g => g.GenreId).ToList();
            result.BookInstances = input.BookInstances.Select(bi => _bookInstanceMapper.Map(bi)).ToList();
            result.AuthorsDto = input.BookAuthors.Select(a => _authorMapper.Map(a.Author)).ToList();
            return result;
        }
    }
}
