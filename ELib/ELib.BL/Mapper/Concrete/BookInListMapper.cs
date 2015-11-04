using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;
using System.Linq;
using System.Collections.Generic;

namespace ELib.BL.Mapper.Concrete
{
    public class BookInListMapper : IMapper<Book, BookInListDto>
    {
        IMapper<Author, AuthorDto> _authorMapper;


        public BookInListMapper(IMapper<Author, AuthorDto> authorMapper)
        {
            _authorMapper = authorMapper;
        }

        public Book Map(BookInListDto input)
        {
            Book result = new Book()
            {
                Id = input.Id,
                Title = input.Title,
                PublisherId = input.PublisherId,
                PublishYear = input.PublishYear,
                SumRatingValue= input.Rating,
                ImageHash = input.ImageHash,
                State = input.State
            };
            return result;
        }

        public BookInListDto Map(Book input)
        {
            BookInListDto result = new BookInListDto()
            {
                Id = input.Id,
                Title = input.Title,
                PublisherId = input.PublisherId,
                PublishYear = input.PublishYear,
                Rating = input.SumRatingValue,
                ImageHash = input.ImageHash,
                State = input.State,
                PublisherName = (input.Publisher == null) ? null : input.Publisher.Name
            };

            result.Authors = input.BookAuthors.Select(ba => _authorMapper.Map(ba.Author)).ToList();

            return result;
        }
    }
}
