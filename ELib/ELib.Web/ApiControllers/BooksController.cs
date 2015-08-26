﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System.Net.Http;
using System.Net;

namespace ELib.Web.ApiControllers
{
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;

        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<BookDto> books = _bookService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
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
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Post(BookDto book)
        {
            try
            {
                _bookService.Insert(book);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(BookDto book)
        {
            try
            {
                _bookService.Update(book);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (NullReferenceException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message); 
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(BookDto book)
        {
            try
            {
                _bookService.Delete(book);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (NullReferenceException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}