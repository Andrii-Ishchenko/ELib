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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace ELib.Web.ApiControllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private readonly ELogger logger;
        private readonly IProfileService _profileService;
        private readonly ISendEmailService _emailService;

        public AccountController(IProfileService profileService, ISendEmailService emailService)
        {
            _profileService = profileService;
            _emailService = emailService;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        /*
        public AccountController(ApplicationUserManager userManager, IProfileService profileService) : this(profileService)
        {
            UserManager = userManager;
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, IProfileService profileService) : this(userManager, profileService)
        { 
            AccessTokenFormat = accessTokenFormat;   
        }*/

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
            string userName = "[A-z0-9-_.]";
            string email = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]";
            string password = "[A - z] +[0 - 9] + ";
            try
            {
                if (model == null || !ModelState.IsValid || !Regex.IsMatch(model.UserName, userName) ||
                       !Regex.IsMatch(model.Email, email, RegexOptions.IgnoreCase) || !Regex.IsMatch(model.Password, password))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Model State Is Not Valid");
                }

                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);
                    var roleResult = UserManager.AddToRole(currentUser.Id, "User");
                    _emailService.SendEmail(model.Email, model.UserName);
                }
                else
                {
                    logger.Error(String.Format("Error In Author/Add, Erros: {0}", result.Errors.ToString()));
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                /* if (!result.Succeeded)
                 {
                     logger.Error(String.Format("Error In Author/Add, Erros: {0}", result.Errors.ToString()));
                     return Request.CreateResponse(HttpStatusCode.BadRequest);
                 }
                 */
                var person = new PersonDto() { ApplicationUserId = user.Id, State = LibEntityState.Added };
                _profileService.Insert(person);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (Exception ex)
            {
                logger.Error("Error In Author/Add", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private IAuthenticationManager Authentication
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }
    }
}