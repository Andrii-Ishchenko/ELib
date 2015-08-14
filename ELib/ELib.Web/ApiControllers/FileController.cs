using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ELib.BL.Services.Abstract;

namespace ELib.Web.ApiControllers
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                string filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                byte[] buffer = await file.ReadAsByteArrayAsync();
                _fileService.SaveImage(buffer);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}