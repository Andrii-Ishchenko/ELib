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
    }
}