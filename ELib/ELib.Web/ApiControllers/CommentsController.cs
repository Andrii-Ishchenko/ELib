using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ELib.BL.DtoEntities;

namespace ELib.Web.ApiControllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet]
        [ActionName("comments-for-book")]
        public HttpResponseMessage GetCommentsByBookId([FromUri] int id, [FromUri] int pageCount = 3, [FromUri] int pageNumb = 1)
        {
            try
            {
                List<CommentDto> commentList = _commentService.GetCommentsByBookId(id, pageCount, pageNumb);
                return Request.CreateResponse(HttpStatusCode.OK, commentList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpGet]
        [ActionName("comment")]
        public HttpResponseMessage GetCommentById(int id)
        {
            try
            {
                CommentDto commentDto = _commentService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, commentDto);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage AddComment(CommentDto commentDto)
        {
            try
            {
                if (commentDto != null && ModelState.IsValid)
                {
                    _commentService.Update(commentDto);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateComment(CommentDto commentDto)
        {
            try
            {
                if (commentDto != null && ModelState.IsValid)
                {
                    var newComment = _commentService.Insert(commentDto);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }        

        [HttpDelete]
        public HttpResponseMessage DeleteCommentById(int id)
        {
            try
            {
                _commentService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}