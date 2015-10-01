using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using ELib.Domain.Entities;
using ELib.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private readonly ELogger _logger;
        private readonly IProfileService _profileService;

        public AccountController(IProfileService profileService)
        {
            _profileService = profileService;
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [AllowAnonymous]
        [Route("register")]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
                }

                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    _logger.Error(String.Format("Error In Author/Add, Erros: {0}", result.Errors.ToString()));
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                var person = new PersonDto() { ApplicationUserId = user.Id };
                _profileService.Insert(person);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Author/Add", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private IAuthenticationManager Authentication
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }
    }
}