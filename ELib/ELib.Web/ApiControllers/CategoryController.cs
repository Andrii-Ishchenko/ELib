using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _service ;
        private ELogger _logger;

        public CategoryController(ICategoryService service)
        {
            _service = service;
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            try
            {
                IEnumerable<CategoryDto> cats = _service.GetAll();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, cats);
            }
            catch(Exception e)
            {
                _logger.Error("Error In Categories/Get", e);
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }          
        }

    }
}