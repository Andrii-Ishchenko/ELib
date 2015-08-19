using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ELib.BL.Services.Abstract;
using System;
using ELib.Common;

namespace ELib.Web.ApiControllers
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;
        private ELogger logger;

        public FileController(IFileService fileService)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _fileService = fileService;
        }

        [HttpPost]
        [ActionName("profile-image")]
        public async Task<HttpResponseMessage> UploadProfileImage()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.Contents)
                {
                    string fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                    byte[] buffer = await file.ReadAsByteArrayAsync();
                    _fileService.SaveProfileImage(buffer, fileName);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error("Error In File/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadBookImage()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.Contents)
                {
                    string fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                    byte[] buffer = await file.ReadAsByteArrayAsync();
                    _fileService.SaveProfileImage(buffer, fileName);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Error("Error In File/UploadBookImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadBookFile()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.Contents)
                {
                    string fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
                    byte[] buffer = await file.ReadAsByteArrayAsync();
                    _fileService.SaveProfileImage(buffer, fileName);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Error("Error In File/UploadBookFile", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}