using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Concrete
{
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        public BookService(IUnitOfWorkFactory factory)
            : base(factory)
        {

        }
    }
}
