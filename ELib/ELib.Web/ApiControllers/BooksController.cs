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
        public HttpResponseMessage Get([FromUri]string query = null,
                                       [FromUri]string authorName = null,
                                       [FromUri]string title = null,
                                       [FromUri]string publisher = null,
                                       [FromUri]string genre = null,
                                       [FromUri]string subgenre = null,
                                       [FromUri]int year = 0,
                                       [FromUri]int pageCount = 3,
                                       [FromUri]int pageNumb = 1)
        {
            try
            {
                SearchDto searchDto = new SearchDto(query, authorName, title, publisher, genre, subgenre, year);
                IEnumerable<BookDto> books = _bookService.GetAll(searchDto, pageCount, pageNumb);
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

        [HttpPost]
        public HttpResponseMessage Post(BookDto book)
        {
            try
            {
                if (book != null && ModelState.IsValid)
                {
                    _bookService.Insert(book);
                    return Request.CreateResponse(HttpStatusCode.OK, new { BookId = book.Id});
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