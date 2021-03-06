﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System.Net.Http;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using ELib.Common;
using System.Linq;


namespace ELib.Web.ApiControllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IBookInListService _bookInListService;
        private readonly ICategoryService _categoryService;
        private ELogger _logger;

        public BooksController(IBookService bookService, IBookInListService bookInListService, ICategoryService categoryService)
        {
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _bookService = bookService;
            _bookInListService = bookInListService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri]int pageCount = 100,
                                       [FromUri]string query = null,
                                       [FromUri]string authorName = null,
                                       [FromUri]string title = null,
                                       [FromUri]string publisher = null,
                                       [FromUri]string genre = null,
                                       [FromUri]int genreId = -1,
                                       [FromUri]int subgenreId = -1,
                                       [FromUri]string subgenre = null,
                                       [FromUri]int year = 0,
                                       [FromUri]int pageNumb = 1,
                                       [FromUri]int categoryId=-1,
                                       [FromUri]string orderBy="AuthorName",
                                       [FromUri]string orderDirection="ASC"
                                       )
        {
            try
            {
                List<int> categoryIds = null;
                if (categoryId > -1)
                {
                    categoryIds = _categoryService.GetAllChildrenForCategory(categoryId).Select(x => x.Id).ToList();
                    categoryIds.Add(categoryId);
                }
                SearchDto searchDto = new SearchDto(query, authorName, title, publisher, genre, subgenre, genreId, subgenreId, year,categoryIds,orderBy,orderDirection);
                IEnumerable<BookInListDto> books = _bookInListService.GetAll(searchDto, pageCount, pageNumb);
                int totalCount = _bookInListService.TotalCount;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, books);
                response.Headers.Add("totalCount", totalCount.ToString());
                return response;
            }
            catch (Exception e)
            {
                _logger.Error("Error In Books/Get", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }

        [HttpGet]
        public HttpResponseMessage GetBooks(int blockId, int pageCount = 6, int pageNumb = 1)
        {
            try
            {
                IEnumerable<BookInListDto> books = null;
                if (blockId == 0)
                { books = _bookInListService.GetBooksWithHighestRating(pageCount, pageNumb); }
                else if (blockId == 1)
                { books = _bookInListService.GetNewBooks(pageCount, pageNumb); }

                int totalCount = _bookInListService.TotalCount;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, books);
                response.Headers.Add("totalCount", totalCount.ToString());
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }

        }
        //[HttpGet]
        //public HttpResponseMessage GetBooksByCategory(int categryId, int pageCount = 5, int pageNumb = 1)
        //{
        //    try
        //    {
        //        List<int> categoryIds = _categoryService.GetAllChildrenForCategory(categryId).Select(x=>x.Id).ToList();
        //        categoryIds.Add(categryId);

        //        IEnumerable<BookDto> books = _bookService.GetBooksForCategory(categoryIds,pageCount,pageNumb);
             
                            
        //        int totalCount = _bookService.TotalCount;
        //        return Request.CreateResponse(HttpStatusCode.OK, new { books, totalCount });
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
        //    }

        //}


        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            try

            {
                BookDto book = _bookService.GetById(id);
                if (book == null)
                    throw new NullReferenceException();
               book.TotalViewCount += 1;
               _bookService.Update(book);
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
                    var newBook = _bookService.Insert(book);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Id = newBook.Id });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch(Exception e)
            {
                _logger.Error("Error Books/add", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
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