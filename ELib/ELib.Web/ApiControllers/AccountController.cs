using ELib.Common;
using ELib.Domain.Entities;
using ELib.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private  ApplicationUserManager _userManager;
        private readonly ELogger logger;

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat) 
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;   
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
              //  return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [AllowAnonymous]
        [ActionName("register")]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    logger.Error(String.Format("Error In Author/Add, Erros: {0}", result.Errors.ToString()));
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Author/Add", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}