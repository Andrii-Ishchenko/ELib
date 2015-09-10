using System;
using System.Collections.Generic;
using System.Web.Http;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System.Net.Http;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using ELib.Common;


namespace ELib.Web.ApiControllers
{
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private ELogger _logger;

        public BooksController(IBookService bookService)
        {
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _bookService = bookService;

        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri]int[]AuthorsIds = null, [FromUri]int pageCount = 3, [FromUri]int pageNumb = 1)
        {
            try
            {
                var queryString = Request.RequestUri.ParseQueryString();
                var queryParams = queryString.Keys;
                Dictionary<string, string> query = new Dictionary<string, string>();
                foreach (var param in queryParams)
                {
                    string key = param.ToString();
                    query.Add(key, queryString[key]);
                }
                //var res = query["AuthorsIds"];
                //ParameterExpression parameter = Expression.Parameter(typeof(BookDto), "x");
                //Expression method = null;
                //foreach (var param in queryParams)
                //{
                //    if (typeof(BookDto).GetProperty(param.ToString()) != null)
                //    {
                //        var value = Convert.ChangeType(query[param.ToString()], typeof(BookDto).GetProperty(param.ToString()).PropertyType);

                //        Expression property = Expression.Property(parameter, param.ToString());
                //        Expression target = Expression.Constant(value);
                //        Expression equalsMethod = Expression.Equal(property, target);
                //        method = (method == null) ? equalsMethod : Expression.And(method, equalsMethod);

                //    }
                //}
                //if (method != null)
                //{
                //    Expression<Func<BookDto, bool>> lambda = Expression.Lambda<Func<BookDto, bool>>(method, parameter);
                //}
                IEnumerable<BookDto> books = _bookService.GetAll(query, pageCount,pageNumb);
                int totalCount = _bookService.TotalCount;
                return Request.CreateResponse(HttpStatusCode.OK, new { books, totalCount});
            }
            catch (Exception e)
            {
                _logger.Error("Error In Books/Get", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
        [ActionName("book")]
        public HttpResponseMessage Get(int id)
        {
            try

            {
                BookDto book = _bookService.GetById(id);
                if (book == null)
                    throw new NullReferenceException();
                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (NullReferenceException e)
            {
                _logger.Error("Not found In Books/GetById", e);
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                _logger.Error("Error In Books/GetById", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
        [ActionName("books-for-author")]
        public HttpResponseMessage GetForAuthor(int id)
        {
            try
            {
                IEnumerable<BookDto> books = _bookService.GetForAuthor(id);
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/GetForAuthor", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
        [ActionName("books-for-publisher")]
        public HttpResponseMessage GetBooksForPublisher(int id)
        {
            try
            {
                IEnumerable<BookDto> books = _bookService.GetBooksForPublisher(id);
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/GetForPublisher", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(BookDto book)
        {
            try
            {
                if (book != null && ModelState.IsValid)
                {
                    _bookService.Insert(book);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch(Exception e)
            {
                _logger.Error("Error Books/add", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(BookDto book)
        {
            try
            {
                if (book != null && ModelState.IsValid)
                {
                    _bookService.Update(book);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch (NullReferenceException e)
            {
                _logger.Error("Not found for Books/update", e);
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/update", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteBookById(int id)
        {
            try
            {
                _bookService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (NullReferenceException e)
            {
                _logger.Error("Not found for Books/deleteById", e);
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/deleteById", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}