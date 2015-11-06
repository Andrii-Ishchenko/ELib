using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class BookInstanceMapper : IMapper<BookInstance, BookInstanceDto>
    {
        public BookInstance Map(BookInstanceDto input)
        {
            if (input == null)
                return null;
            return new BookInstance()
            {
                Id = input.Id,
                BookId = input.BookId,
                FileHash = input.FileHash,
                FileName = input.FileName,
                DownloadCount = input.DownloadCount,
                State = input.State
            };
        }

        public BookInstanceDto Map(BookInstance input)
        {
            if (input == null)
                return null;
            return new BookInstanceDto()
            {
                Id = input.Id,
                BookId = input.BookId,
                FileHash = input.FileHash,
                FileName = input.FileName,
                DownloadCount = input.DownloadCount,
                State = input.State
            };
        }
    }
}
