using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Mapper.Concrete
{
    public class BookInstanceMapper : IMapper<BookInstance, BookInstanceDto>
    {
        public BookInstance Map(BookInstanceDto input)
        {
            return new BookInstance()
            {
                Id = input.Id,
                BookId = input.BookId,
                FileHash = input.FileHash,
                FileName = input.FileName,
                DownloadCount = input.DownloadCount
            };
        }

        public BookInstanceDto Map(BookInstance input)
        {
            return new BookInstanceDto()
            {
                Id = input.Id,
                BookId = input.BookId,
                FileHash = input.FileHash,
                FileName = input.FileName,
                DownloadCount = input.DownloadCount
            };
        }
    }
}
