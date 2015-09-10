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
                string id = User.Identity.GetUserId();

                CurrentPersonDto person = _profileService.GetByApplicationUserId(id);
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

        [HttpPut]
        public HttpResponseMessage UpdateCurrentUser(CurrentPersonDto person)
        {
            try
            {   
                if (ModelState.IsValid && person != null)
                {

                    string id = User.Identity.GetUserId();

                    CurrentPersonDto thisPerson = _profileService.GetByApplicationUserId(id);
                    if (thisPerson == null)
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);

                    person.ApplicationUserId = id;
                    person.Id = thisPerson.Id;
                    person.UserName = thisPerson.UserName;
                    _profileService.Update(person);
                }
                
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}