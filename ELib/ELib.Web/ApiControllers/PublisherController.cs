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

namespace ELib.Web.ApiControllers
{
    public class PublisherController: ApiController
    {
        private readonly IPublisherService _service;
        private ELogger logger;

        public PublisherController(IPublisherService service)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _service = service;           
        }
        
        [HttpGet]
        public HttpResponseMessage GetPublishers()
        {
            try
            {
                var publishers = _service.GetAll();               
                return Request.CreateResponse(HttpStatusCode.OK, publishers);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Publisher/Get",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPublisherById(int id)
        {
            try
            {
                var publishers = _service.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, publishers);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Publisher/GetById",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddPublisher(PublisherDto publisher)
        {
            try
            {
                if (publisher!=null && ModelState.IsValid)
                {
                    _service.Insert(publisher);
                    return Request.CreateResponse(HttpStatusCode.OK, publisher);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch(Exception ex)
            {
                logger.Error("Error In Publisher/Add",ex);
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
                    _service.Update(publisher);
                    return Request.CreateResponse(HttpStatusCode.OK, publisher);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Publisher/Update",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeletePublisher(PublisherDto publisher)
        {
            try
            {
                if (publisher != null && ModelState.IsValid)
                {
                    _service.Delete(publisher);
                    return Request.CreateResponse(HttpStatusCode.OK, publisher);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Publisher/Delete", ex);
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
                    _service.DeleteById(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Publisher/DeleteById",ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}