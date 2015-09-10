using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Abstract
{
    public interface IBookService : IBaseService<Book, BookDto>
    {
        IEnumerable<BookDto> GetForAuthor(int idAuthor);
        IEnumerable<BookDto> GetAll(Dictionary<string,string>query, int pageCount, int pageNumb);
        IEnumerable<BookDto> GetBooksForPublisher(int id);
    }
}
