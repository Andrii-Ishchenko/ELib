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
using System.Net.Http.Headers;
using System.Drawing;
using System.Web.Hosting;
using System.Drawing.Imaging;

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
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "User Not Found");
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
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("author-image")]
        public async Task<HttpResponseMessage> UploadAuthorImage(int id)
        {
            try
            {

                UnitOfWorkFactory uowf = new UnitOfWorkFactory();
                Person p;
                using (var uow = uowf.Create())
                {
                    string authId = User.Identity.GetUserId();
                    p = uow.Repository<Person>().Get(pers => pers.ApplicationUserId == authId).FirstOrDefault();
                    if (p == null)
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "User Not Found");
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
                        saveResult = _fileService.SaveAuthorImage(buffer, fileName, id,userId);
                    }

                    if (saveResult)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                logger.Error("Error In Files/UploadAuthorImage");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/UploadProfileImage", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

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
                        string fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
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

        // maybe should use post method (becouse download counter change system)
        [HttpGet]
        [ActionName("book-download")]
        public HttpResponseMessage GetBookFile(string id)
        {
            try
            {
                HttpResponseMessage result = null;

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(_fileService.GetBookFile(id));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = _fileService.GetBookFileName(id);

                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error In Files/DownloadBookFile", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpGet]
        [ActionName("book-images")]
        public HttpResponseMessage GetBookImage(string hash, int w=0, int h=0)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            byte[] image = null;
            if (hash != "" && hash != null)
                image = _fileService.GetBookImage(hash, w, h);

            if (image == null)
            {
                String rootpath = HostingEnvironment.MapPath("~/Content/");
                var path = Path.Combine(rootpath, "no-photo.png");
                Image i = Image.FromFile(path);
                using(MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms,ImageFormat.Png);
                    image = ms.ToArray();
                }
            }

            message.Content = new ByteArrayContent(image);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            message.StatusCode = HttpStatusCode.OK;
            return message;

        }

        [HttpGet]
        [ActionName("profile-image")]
        public HttpResponseMessage GetProfileImage(string hash, int w=0, int h=0)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            byte[] image = null;
            if (hash != "" && hash != null)
                image = _fileService.GetProfileImage(hash, w, h);

            if (image == null)
            {
                String rootpath = HostingEnvironment.MapPath("~/Content/");
                var path = Path.Combine(rootpath, "no-photo.png");
                Image i = Image.FromFile(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms, ImageFormat.Png);
                    image = ms.ToArray();
                }
            }
            message.Content = new ByteArrayContent(image);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }

        [HttpGet]
        [ActionName("author-image")]
        public HttpResponseMessage GetAuthorImage(string hash, int w=0,int h = 0)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            byte[] image = null;
            if (hash != "" && hash != null)
                image = _fileService.GetAuthorImage(hash, w, h);


            if (image == null)
            {
                String rootpath = HostingEnvironment.MapPath("~/Content/");
                var path = Path.Combine(rootpath, "no-photo.png");
                Image i = Image.FromFile(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms, ImageFormat.Png);
                    image = ms.ToArray();
                }
            }
     
            message.Content = new ByteArrayContent(image);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }
    }
}