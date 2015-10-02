using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELib.Web.ApiControllers
{
    public class LanguagesController : ApiController
    {
        private readonly ILanguageService _languageService;
        private ELogger _logger;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
            _logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        [HttpGet]
        public HttpResponseMessage GetLanguages()
        {
            try
            {
                var languages = _languageService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, languages);
            }
            catch(Exception ex)
            {
                _logger.Error("Error In Languages/Get");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetLanguageById(int id)
        {
            try
            {
                var language = _languageService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, language);
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Language/GetById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddLanguage(LanguageDto language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newLanguage = _languageService.Insert(language);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Language/Add");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateLanguage(LanguageDto language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _languageService.Update(language);
                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Language/Update");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteLanguageById(int id)
        {
            try
            {
                _languageService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                _logger.Error("Error In Language/DeleteById");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}