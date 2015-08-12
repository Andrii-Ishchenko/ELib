using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class GenreController : ApiController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public HttpResponseMessage GetGenres()
        {
            try
            {
                var genres = _genreService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, genres);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetGenreById(int id)
        {
            try
            {
                var genre = _genreService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, genre);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddGenre(GenreDto genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _genreService.Insert(genre);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateGenre(GenreDto genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _genreService.Update(genre);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteGenre(int id)
        {
            try
            {
                _genreService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}