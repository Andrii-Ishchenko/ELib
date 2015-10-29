using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ELib.Common;
using ELib.Domain.Entities;

namespace ELib.Web.ApiControllers
{
    public class PublishersController: ApiController
    {
        private readonly IPublisherService _pubisherService;
        private readonly IBookInListService _bookInListService;
        private ELogger _logger;

        public PublishersController(IPublisherService service, IBookInListService bookInListService)
        {
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _pubisherService = service;
            _bookInListService = bookInListService;
        }
        
        [HttpGet]
        public HttpResponseMessage GetPublishers([FromUri]string query = null,
                                                 [FromUri]int pageCount = 3,
                                                 [FromUri]int pageNumb = 1,
                                                 [FromUri]string orderBy = "Name",
                                                 [FromUri]string orderDirection = "ASC")
        {
             try
             {
                IEnumerable<PublisherDto> publishers = _pubisherService.GetAll(query, pageCount, pageNumb, orderBy,orderDirection);
                int totalCount = _pubisherService.TotalCount;
                return Request.CreateResponse(HttpStatusCode.OK, new { publishers, totalCount });
            }
             catch (Exception ex)
             {
                 _logger.Error("Error In Publisher/Get",ex);
                 return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
             }
        }

        [HttpGet]
        [ActionName("publisher")]
        public HttpResponseMessage GetPublisherById(int id)
        {
             try
             {
                var publishers = _pubisherService.GetById(id);
                 return Request.CreateResponse(HttpStatusCode.OK, publishers);
             }
             catch (Exception ex)
             {
                 _logger.Error("Error In Publisher/GetById",ex);
                 return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
             }           
        }

        [HttpPost]
        [ActionName("new")]
        public HttpResponseMessage AddPublisher(PublisherDto publisher)
        {
            try
            {
                if (publisher!=null && ModelState.IsValid)
                {
                    var newPublisher = _pubisherService.Insert(publisher);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Id = newPublisher.Id });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch(Exception ex)
            {
                _logger.Error("Error In Publisher/Add",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }          
        }

        [HttpPut]
        public HttpResponseMessage UpdatePublisher(PublisherDto publisher)
        {
            try
            {
                if (publisher != null && ModelState.IsValid)
                {
                    _pubisherService.Update(publisher);
                    return Request.CreateResponse(HttpStatusCode.OK, publisher);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Publisher/Update",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [HttpDelete]
        public HttpResponseMessage DeletePublisherById(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _pubisherService.DeleteById(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Publisher/DeleteById",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpGet]
        [ActionName("books")]
        public HttpResponseMessage GetBooks(int id)
        {
            try
            {
                IEnumerable<BookInListDto> books = _bookInListService.GetBooksForPublisher(id);
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            catch (Exception e)
            {
                _logger.Error("Error Books/GetForPublisher", e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}