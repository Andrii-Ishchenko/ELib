using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ELib.Common;

namespace ELib.Web.ApiControllers
{
    public class RatingsController : ApiController
    {
        private readonly IRatingService _ratingService;
        private ELogger logger;

        public RatingsController(IRatingService ratingService)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _ratingService = ratingService;
        }

        [HttpGet]
        public HttpResponseMessage GetRatings()
        {
            try
            {
                var ratings = _ratingService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, ratings);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Rating/Get");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetGenreById(int id)
        {
            try
            {
                var genre = _ratingService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, genre);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Rating/GetById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddRating(RatingBookDto rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ratingService.AddRating(rating);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Rating/Add");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateRating(RatingBookDto rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ratingService.Update(rating);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Rating/Update");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteRatingById(int id)
        {
            try
            {
                _ratingService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Rating/DeleteById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}