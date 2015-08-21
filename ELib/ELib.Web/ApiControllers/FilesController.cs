using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ELib.BL.Services.Abstract;
using System;
using ELib.Common;

namespace ELib.Web.ApiControllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;
        private ELogger logger;

        public FilesController(IFileService fileService)
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
            _fileService = fileService;
        }

        // HttpPost used for test
        [HttpPost]
        [ActionName("profile-images")]
        public async Task<HttpResponseMessage> UploadProfileImage()
        {
            try
            {
                // Hard code (should use current user id from Identity)
                int userId = 1;

                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    bool saveResult = false;
                    

                    foreach (var file in provider.Contents)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        byte[] buffer = await file.ReadAsByteArrayAsync();
                        saveResult = _fileService.SaveProfileImage(buffer, fileName, userId);
                    }

                    if (saveResult)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                logger.Error("Error In Files/UploadProfileImage");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                logger.Error("Error In Files/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // HttpPost used for test
        // Maybe should use "model" instead "id", and move method to another controller
        [HttpPost]
        [ActionName("book-images")]
        public async Task<HttpResponseMessage> UploadBookImage(int id)
        {
            try
            {
                // Hard code (should use current user id from Identity)
                int userId = 1;

                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    bool saveResult = false;


                    foreach (var file in provider.Contents)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        byte[] buffer = await file.ReadAsByteArrayAsync();
                        saveResult = _fileService.SaveBookImage(buffer, fileName, id, userId);
                    }

                    if (saveResult)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                logger.Error("Error In Files/UploadProfileImage");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // HttpPost used for test
        // Maybe should use "model" instead "id", and move method to another controller
        [HttpPost]
        [ActionName("book-instances")]
        public async Task<HttpResponseMessage> UploadBookFile(int id)
        {
            try
            {
                // Hard code (should use current user id from Identity)
                int userId = 1;

                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    bool saveResult = false;


                    foreach (var file in provider.Contents)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        byte[] buffer = await file.ReadAsByteArrayAsync();
                        saveResult = _fileService.SaveBookFile(buffer, fileName, id, userId);
                    }

                    if (saveResult)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                logger.Error("Error In Files/UploadProfileImage");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}