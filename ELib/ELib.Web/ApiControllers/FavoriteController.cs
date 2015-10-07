using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class FavoriteController  :ApiController
    {
        private IFavoriteService _service;

        private IProfileService _profileService;
        private ELogger _logger;

        public FavoriteController(IFavoriteService service)
        {
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _service = service;
        } 

        public HttpResponseMessage ToggleFavorite(int bookId)
        {

        }

        public HttpResponseMessage IsInFavorites(int bookId)
        {
            try
            {
                string id = User.Identity.GetUserId();
                PersonDto person = _profileService.GetById(id);
                if (person == null)
                    throw new NullReferenceException();

                bool result = _service.IsInFavorite(bookId, person.Id);
                return Request.CreateResponse(HttpStatusCode.OK, result);

                //var book = _bookService.GetById(bookId);
                //if (book == null)
                //{
                //    return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "book not found");
                //}
                //var favorite = book.Favorites.Where(f => f.UserId == id).FirstOrDefault();


            }
            catch(Exception e)
            {
                _logger.Error("Error in FavoriteController", e);
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest,e.Message);
            }
        }
    
    }
}