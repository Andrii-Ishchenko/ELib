using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;

namespace ELib.BL.Services.Concrete
{
    public class BookService : BaseService<Book, BookDto>, IBookService
    {
        public BookService(IUnitOfWorkFactory factory)
            : base(factory)
        {

        }
        public IEnumerable<BookDto> GetForAuthor(int idAuthor)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                var entities = uow.Repository<BookAuthor>().Get(x=>x.AuthorId==idAuthor).Select(y=>y.Book).OrderByDescending(rating=>rating.SumRatingValue);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        /* public IEnumerable<BookDto> GetForPublisher(int idPublisher)
        {
           using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                List<Book> entities = uow.Repository<Publisher>().Get(x => x.Id == idPublisher).Select(y => y.Books);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
    }*/

        public IEnumerable<BookDto> GetAll(int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                
                    var entities = uow.Repository<Book>().Get(skipCount : pageCount * (pageNumb - 1), topCount: pageCount);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }
    }
}
