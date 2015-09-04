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

namespace ELib.Web.ApiControllers
{
    public class CurrentProfileController : ApiController
    {
        private readonly ICurrentProfileService _profileService;
        private ELogger logger;

        public CurrentProfileController(ICurrentProfileService service)
        {
            _profileService = service;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        [HttpGet]
        public HttpResponseMessage GetCurrentUser()
        {
            try
            {
                //ABSOLUTELY WRONG!! use IDENTITY FOR getting applicationuser and getting currentuser
                CurrentPersonDto person = _profileService.GetById(5);
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

        [HttpPost]
        public HttpResponseMessage PostCurrentUser(CurrentPersonDto person)
        {
            //try
            //{
            //    _profileService.Update(person);
            //}
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}