using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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

        public PersonDto GetCurrentUser()
        {
            ///!!!!
            /// 
            PersonDto person = new PersonDto();
            person.Id = 123;
            person.FirstName = "Синьйор";
            person.LastName = "Дотнетченко";
            person.Email = "ceo@microsoft.com";
            person.Login = "dotNetHero";
            person.Phone = "+31195982000";
            return person;
        }
    }
}