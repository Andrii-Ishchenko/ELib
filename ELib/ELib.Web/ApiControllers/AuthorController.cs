using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public HttpResponseMessage GetAuthors()
        {
            try
            {
                IEnumerable<AuthorDto> authors = _authorService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, authors);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAuthorById(object id)
        {
            try
            {
                AuthorDto authorDto = _authorService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, authorDto);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage CreateAuthor(AuthorDto authorDto)
        {
            try
            {
                _authorService.Insert(authorDto);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateAuthor(AuthorDto authorDto)
        {
            try
            {
                _authorService.Update(authorDto);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAuthor(AuthorDto authorDto)
        {
            try
            {
                _authorService.Delete(authorDto);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAuthorById(object id)
        {
            try
            {
                _authorService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
