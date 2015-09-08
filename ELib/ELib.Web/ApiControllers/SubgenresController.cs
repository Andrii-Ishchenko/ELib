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
    public class SubgenresController : ApiController
    {
        private readonly ISubgenreService _subgenresService;

        public SubgenresController(ISubgenreService subgenresService)
        {
            _subgenresService = subgenresService;
        }

        public SubgenresController(){ }

        [HttpGet]
        public HttpResponseMessage GetSubgenres()
        {
            try
            {
                var subgenres = _subgenresService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, subgenres);               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddSubgenres(SubgenreDto subgenre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subgenresService.Insert(subgenre);
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
        public HttpResponseMessage UpdateSubgenre(SubgenreDto subgenre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subgenresService.Update(subgenre);
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
        public HttpResponseMessage DeleteSubgenreById(int id)
        {
            try
            {
                _subgenresService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}