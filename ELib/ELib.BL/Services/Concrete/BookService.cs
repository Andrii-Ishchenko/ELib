using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;

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
                var entities = uow.Repository<BookAuthor>().Get(x => x.AuthorId == idAuthor).Select(y => y.Book).OrderByDescending(rating => rating.SumRatingValue);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetBooksForPublisher(int id)
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();
                //var entities = uow.Repository<Book>().Get(x => x.PublisherId == id).OrderByDescending(rating => rating.SumRatingValue);
                var entities = uow.Repository<Book>().Get(x => x.PublisherId == id);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public IEnumerable<BookDto> GetAll(Dictionary<string,string>query, int pageCount, int pageNumb)
        {
            Expression<Func<Book, bool>> expression = buildExpression(query);
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<BookDto>();

                var entities = uow.Repository<Book>().Get(skipCount: pageCount * (pageNumb - 1), topCount: pageCount);

                foreach (var item in entities)
                {
                    var entityDto = AutoMapper.Mapper.Map<BookDto>(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        private Expression<Func<Book, bool>> buildExpression(Dictionary<string, string> query)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(BookDto), "x");
            Expression method = null;
            foreach (KeyValuePair<string,string>item in query)
            {
                if (typeof(BookDto).GetProperty(item.Key) != null)
                {
                    var value = castType(item.Value, typeof(BookDto).GetProperty(item.Key).PropertyType);

                    Expression property = Expression.Property(parameter, item.Key);
                    Expression target = Expression.Constant(value, typeof(BookDto).GetProperty(item.Key).PropertyType);
                    Expression equalsMethod = Expression.Equal(property, target);
                    method = (method == null) ? equalsMethod : Expression.And(method, equalsMethod);

                }
            }
            Expression<Func<BookDto, bool>> lambda;
            if (method != null)
            {
               lambda = Expression.Lambda<Func<BookDto, bool>>(method, parameter);
            }
            return null; ////
        }

        private object castType(string value, Type propertyType)
        {
            
          if (propertyType == typeof(string))
            {
                return value;
            }

          if (propertyType == typeof(int))
            {
                int res;
                if (Int32.TryParse(value, out res))
                {
                    return res;
                }
                else throw new InvalidCastException();
            }

          if (propertyType == typeof(int?))
            {
                int res;
                if (Int32.TryParse(value, out res))
                {
                    return res as int?;
                }
                else return null;
            }


          if (propertyType == typeof(DateTime?))
            {
                DateTime res;
                if (DateTime.TryParse(value, out res))
                    return res;
                int ires;
                if (Int32.TryParse(value, out ires))
                    return new DateTime(ires, 1, 1) as DateTime?;
                throw new InvalidCastException();
            }

          if (propertyType == typeof(ICollection<string>))
            {
                return value.Split(',');
            }

            if (propertyType == typeof(ICollection<int>))
            {
                string[]str = value.Split(',');
                List<int> res = new List<int>();
                foreach (string item in str)
                {
                    int intRes;
                    if (Int32.TryParse(item, out intRes))
                    {
                        res.Add(intRes);
                    }
                    else throw new InvalidCastException();
                }
                return res;
            }
            throw new FormatException();
        }
    }
}