using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace ELib.Web.ApiControllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;
        private ELogger logger;

        public ProfileController(IProfileService service)
        {
            _profileService = service;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetCurrentUser()
        {
            try
            {
                //DONT FORGET REFACTOR THIS
                string id = User.Identity.GetUserId();
                PersonDto person = _profileService.GetById(id);
                if (person == null)
                    throw new NullReferenceException();
                return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (NullReferenceException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}