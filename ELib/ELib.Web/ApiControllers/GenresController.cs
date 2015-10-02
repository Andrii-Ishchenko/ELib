using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ELib.Common;
using Microsoft.AspNet.Identity;

namespace ELib.Web.ApiControllers
{
    public class GenresController : ApiController
    {
        private readonly IGenreService _genreService;
        private ELogger logger;

        public GenresController(IGenreService genreService)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
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
                logger.Error("Error In Genre/Get");
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
                logger.Error("Error In Genre/GetById");
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
                    var newGenre = _genreService.Insert(genre);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Genre/Add");
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
                logger.Error("Error In Genre/Update");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteGenreById(int id)
        {
            try
            {
                _genreService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Genre/DeleteById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}