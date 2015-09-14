using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ELib.BL.Services.Abstract;
using System;
using ELib.Common;
using ELib.DAL.Infrastructure.Concrete;
using Microsoft.AspNet.Identity;
using ELib.Domain.Entities;
using System.IO;
using System.Web;

namespace ELib.Web.ApiControllers
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;
        private ELogger logger;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        // HttpPost used for test
        [HttpPost]
        [ActionName("profile-image")]
        public async Task<HttpResponseMessage> UploadProfileImage()
        {
            try
            {
                // Hard code (should use current user id from Identity)
                UnitOfWorkFactory uowf = new UnitOfWorkFactory();
                Person p;
                using (var uow = uowf.Create())
                {
                    string id = User.Identity.GetUserId();
                    p = uow.Repository<Person>().Get(pers => pers.ApplicationUserId == id).FirstOrDefault();
                    if (p == null)
                        return Request.CreateResponse(HttpStatusCode.BadRequest,"User Not Found");
                }
                int userId = p.Id;

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
        [ActionName("book-image")]
        [Authorize]
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

                logger.Error("Error In Files/UploadBookImage");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadBookImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // HttpPost used for test
        // Maybe should use "model" instead "id", and move method to another controller
        [HttpPost]
        [ActionName("book-instance")]
        [Authorize]
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

                logger.Error("Error In Files/UploadBookFile");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadBookFile", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // HttpPost used for test
        // Maybe should use "model" instead "id", and move method to another controller
        [HttpGet]
        [ActionName("book-download")]
        public HttpResponseMessage GetBookFile(string id)
        {
            try
            {
                HttpResponseMessage result = null;
                string directoryPath = String.Format("{0}\\{1}\\{2}", @"E:\LibraryContent\BookFiles", 
                    id.Substring(0,2), id.Substring(2,2));

                string filePath = Directory.GetFiles(directoryPath, String.Format("{0}.*", id)).First();

                if (!File.Exists(filePath))
                { 
                    logger.Error("Error In Files/DownloadBookFile");
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "SampleImg";
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/DownloadBookFile", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}