using NewBlogAPI.Components.BlogArticleFilesComponents;
using NewBlogAPI.Components.BlogArticleFilesComponents.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

#if FILES_TO_FILESYSTEM
    using System.Web;
#endif

namespace NewBlogAPI.Controllers.ApiControllers
{
    //[System.Web.Mvc.Authorize]
    public class BlogArticleFilesController : ApiController
    {
        private readonly IBlogFilesService _fileService;

        public BlogArticleFilesController(IBlogFilesService inService)
        {
            _fileService = inService;
        }

        [HttpDelete]
        [Route("Api/ArticleFiles/DeleteArticleFiles")]
        public async Task<IHttpActionResult> DeleteArticleFiles(long inArticleId)
        {

#if FILES_TO_FILESYSTEM
            System.SystemException error = await _fileService.
                                                    DeleteArticleFiles( inArticleId, 
                                                                        HttpContext.
                                                                            Current.
                                                                            Server.
                                                                            MapPath("~/Files"));
#else
            System.SystemException error = await _fileService.DeleteArticleFiles(inArticleId);
#endif

            if (error == null)
                return Ok();
            else
                return BadRequest(error.Message);
        }

        [HttpDelete]
        [Route("Api/ArticleFiles/DeleteFile")]
        public async Task<IHttpActionResult> DeleteFile(long inArticleId, string inFileName)
        {

#if FILES_TO_FILESYSTEM
            System.SystemException error = await _fileService.
                                                    DeleteFile( inFileName, 
                                                                inArticleId,   
                                                                HttpContext.
                                                                    Current.
                                                                    Server.
                                                                    MapPath("~/Files"));
#else
            System.SystemException error = await _fileService.DeleteFile(inFileName, inArticleId);
#endif

            if (error == null)
                return Ok();
            else
                return BadRequest(error.Message);
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("Api/ArticleFiles/GetFilesNames")]
        public async Task<IHttpActionResult> GetFilesNames(long inArticleId)
        {
            try
            {

#if FILES_TO_FILESYSTEM
                List<string> respond = await _fileService.GetFilesList(inArticleId, HttpContext.
                                                                                        Current.
                                                                                        Server.
                                                                                        MapPath("~/Files"));
#else
                List<string> respond = await _fileService.GetFilesList(inArticleId);
#endif

                return Ok(respond);
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("Api/ArticleFiles/DownloadFile")]
        public async Task<HttpResponseMessage> DownloadFile(long inArticleId, string inFileName)
        {
            try
            {

#if FILES_TO_FILESYSTEM
                BlogArticleFile fileEntitie = await _fileService.ExtractFile(   inArticleId, 
                                                                                inFileName,   
                                                                                HttpContext.
                                                                                    Current.
                                                                                    Server.
                                                                                    MapPath("~/Files"));
#else
                BlogArticleFile fileEntitie = await _fileService.ExtractFile(inArticleId, inFileName);
#endif

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(fileEntitie.FileContent)
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileEntitie.FileType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileEntitie.FileName
                };

                return response;
            }
            catch (System.SystemException error)
            {
                throw new HttpResponseException
                         (
                             Request.
                             CreateResponse
                                 (
                                     HttpStatusCode.InternalServerError,
                                     error.Message
                                 )
                         );
            }
        }

        [HttpPost]
        [Route("Api/ArticleFiles/UploadFiles")]
        public async Task<HttpResponseMessage> UploadFiles(long inArticleId)
        {
            if(Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = await Request.
                                            Content.
                                            ReadAsMultipartAsync(new MultipartMemoryStreamProvider());

#if FILES_TO_FILESYSTEM
                System.SystemException error = await _fileService.PutFiles( streamProvider, 
                                                                            inArticleId, 
                                                                            HttpContext.
                                                                                Current.
                                                                                Server.
                                                                                MapPath("~/Files"));
#else
                System.SystemException error = await _fileService.PuFiles(streamProvider, inArticleId);
#endif

                if (error != null)
                    throw new HttpResponseException
                        (
                            Request.
                            CreateResponse
                                (
                                    HttpStatusCode.InternalServerError,
                                    error.Message
                                )
                        );
            }
            else
            {
                throw new HttpResponseException
                    (
                        Request.
                        CreateResponse
                            (
                                HttpStatusCode.NotAcceptable,
                                "This request is not properly formatted"
                            )
                    );
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}