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
    public class RatingCommentController : ApiController
    {
        private readonly IRatingCommentService _ratingCommentService;
        private ELogger logger;

        public RatingCommentController(IRatingCommentService ratingCommentService)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _ratingCommentService = ratingCommentService;
        }

        [HttpGet]
        public HttpResponseMessage GetRatingComments()
        {
            try
            {
                var ratings = _ratingCommentService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, ratings);
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/Get");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRatingCommentById(int id)
        {
            try
            {
                var genre = _ratingCommentService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, genre);
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/GetById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddRatingComment(RatingCommentDto ratingComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ratingCommentService.AddLike(ratingComment);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/Add");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateRatingComment(RatingCommentDto ratingComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ratingCommentService.Update(ratingComment);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/Update");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteRatingCommentById(int id)
        {
            try
            {
                _ratingCommentService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/DeleteById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteRatingComment(RatingCommentDto ratingComment)
        {
            try
            {
                _ratingCommentService.Delete(ratingComment);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                logger.Error("Error In RatingComment/Delete");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}