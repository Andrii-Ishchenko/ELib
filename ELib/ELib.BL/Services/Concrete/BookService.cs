using System;
using System.Collections.Generic;
using System.Linq;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        public BookService(IUnitOfWorkFactory factory, IMapper<Book, BookDto> mapper)
            : base(factory, mapper)
        {

        }
    }
}