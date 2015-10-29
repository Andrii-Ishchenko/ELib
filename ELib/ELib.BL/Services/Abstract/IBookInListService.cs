using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Abstract
{
    public interface IBookInListService : IBaseService<Book, BookInListDto>
    {
        IEnumerable<BookInListDto> GetForAuthor(int idAuthor);
        IEnumerable<BookInListDto> GetAll(SearchDto searchDto, int pageCount, int pageNumb);
        IEnumerable<BookInListDto> GetBooksWithHighestRating(int pageCount, int pageNumb);
        IEnumerable<BookInListDto> GetNewBooks(int pageCount, int pageNumb);
        IEnumerable<BookInListDto> GetBooksForPublisher(int id);
    }
}