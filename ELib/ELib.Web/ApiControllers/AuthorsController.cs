﻿using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService _authorService;
        private readonly IBookInListService _bookInListService;
        private ELogger _logger;

        public AuthorsController(IAuthorService authorService, IBookInListService bookInListService)
        {
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _authorService = authorService;
            _bookInListService = bookInListService;
        }


        [HttpGet]
        public HttpResponseMessage Get([FromUri]int pageCount = 100,
                                       [FromUri]string query = null,
                                       [FromUri]string authorName = null,
                                       [FromUri]int year = 0,
                                       [FromUri]int pageNumb = 1,
                                       [FromUri]string orderBy = "LastName",
                                       [FromUri]string orderDirection = "ASC")
        {
            try
            {
                IEnumerable<AuthorDto> authors = _authorService.GetAll(query, authorName, orderBy, orderDirection, year, pageNumb, pageCount);
                int totalCount = _authorService.TotalCount;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, authors);
                response.Headers.Add("totalCount", totalCount.ToString());
                return response;
            }
            catch (Exception e)
            {
                _logger.Error("Error In Authors/Get", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }



        [HttpGet]
        [ActionName("author")]
        public HttpResponseMessage GetAuthorById(int id)
        {
            try
            {
                AuthorDto authorDto = _authorService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, authorDto);
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Author/GetById", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddAuthor(AuthorDto authorDto)
        {
            try
            {
                if (authorDto != null && ModelState.IsValid)
                {
                    var newAuthor = _authorService.Insert(authorDto);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Id = newAuthor.Id });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Author/Add", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateAuthor(AuthorDto authorDto)
        {
            try
            {
                if (authorDto != null && ModelState.IsValid)
                {
                    _authorService.Update(authorDto);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Author/Update", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAuthorById(int id)
        {
            try
            {
                _authorService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Author/DeleteById", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("books")]
        public HttpResponseMessage GetBooks(int id)
        {
            try
            {
                IEnumerable<BookInListDto> books = _bookInListService.GetForAuthor(id);
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/GetForAuthor", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
