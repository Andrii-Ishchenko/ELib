using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class PublisherController: ApiController
    {
        private readonly IPublisherService _service;

        public PublisherController(IPublisherService service)
        {
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public HttpResponseMessage GetPublisherById(int id)
        {
            try
            {
                var publishers = _service.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, publishers);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }          
        }

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public HttpResponseMessage DeletePublisherById(object id)
        {
            try
            {
                if (id != null && ModelState.IsValid)
                {
                    _service.DeleteById(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State is not valid.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}