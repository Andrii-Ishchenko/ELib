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
    }
}
