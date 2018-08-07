using NewBlogAPI.Components.BlogArticleComponents;
using NewBlogAPI.Components.BlogArticleComponents.Services.Interfaces;
using NewBlogAPI.Components.BlogArticleComponents.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

#if FILES_TO_FILESYSTEM
    using System.Web;
#endif

namespace NewBlogAPI.Controllers.ApiControllers
{
    //[System.Web.Mvc.Authorize]
    public class BlogArticleController : ApiController
    {
        private readonly IBlogArticleService _blogService;

        public BlogArticleController(IBlogArticleService blogService)
        {
            _blogService = blogService;
        }

        //***       Articles

        [HttpPost]
        [ResponseType(typeof(BlogArticle))]
        [Route("Api/BlogArticle/CreateArticle")]
        public async Task<IHttpActionResult> CreateArticle(BlogArticle blogArticle)
        {
            try
            {

#if FILES_TO_FILESYSTEM

                return Ok(await _blogService.CreateArticle( blogArticle, 
                                                            HttpContext.
                                                                Current.
                                                                Server.
                                                                MapPath("~/Files")));
#else
                return Ok(await _blogService.CreateArticle(blogArticle));
#endif
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<BlogArticle>))]
        [Route("Api/BlogArticle/GetAllArticles")]
        public async Task<IHttpActionResult> GetAllArticles()
        {
            try
            {
                return Ok(await _blogService.GetAllArticles());
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(BlogArticle))]
        [Route("Api/BlogArticle/GetArticle")]
        public async Task<IHttpActionResult> GetArticle(long inArticleId)
        {
            try
            {
                return Ok(await _blogService.GetArticle(inArticleId));
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(BlogArticle))]
        [Route("Api/BlogArticle/DeleteArticle")]
        public async Task<IHttpActionResult> DeleteArticle(long inArticleId)
        {
            try
            {

#if FILES_TO_FILESYSTEM
                return Ok(await _blogService.DeleteArticle( inArticleId, 
                                                            HttpContext.
                                                                Current.
                                                                Server.
                                                                MapPath("~/Files")));
#else
                return Ok(await _blogService.DeleteArticle(inArticleId));
#endif
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPatch]
        [Route("Api/BlogArticle/UpdateArticle")]
        public async Task<IHttpActionResult> UpdateArticle(BlogArticle inArticle)
        {
            try
            {
                await _blogService.UpdateArticle(inArticle);
                return Ok();
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        //***       Tags

        [HttpPost]
        [Route("Api/BlogArticle/AddTag")]
        public async Task<IHttpActionResult> AddTag(BlogArticleTag inTag)
        {
            try
            {
                await _blogService.AddTag(inTag);
                return Ok();
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [Route("Api/BlogArticle/DeleteTag")]
        public async Task<IHttpActionResult> DeleteTag(BlogArticleTag inTag)
        {
            try
            {
                await _blogService.DeleteTag(inTag);
                return Ok();
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(List<string>))]
        [Route("Api/BlogArticle/DeleteArticleTags")]
        public async Task<IHttpActionResult> DeleteArticleTags(long inBlogArticleId)
        {
            try
            {
                return Ok(await _blogService.DeleteArticleTags(inBlogArticleId));
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(List<string>))]
        [Route("Api/BlogArticle/DeleteAllTags")]
        public async Task<IHttpActionResult> DeleteAllTags()
        {
            try
            {
                return Ok(await _blogService.DeleteAllTags());
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("Api/BlogArticle/GetArticleTags")]
        public async Task<IHttpActionResult> GetArticleTags(long inBlogArticleId)
        {
            try
            {
                return Ok(await _blogService.GetArticleTags(inBlogArticleId));
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("Api/BlogArticle/GetAllTags")]
        public async Task<IHttpActionResult> GetAllTags()
        {
            try
            {
                return Ok(await _blogService.GetAllTags());
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }

        //***       Comments

        [HttpPost]
        [Route("Api/BlogArticle/AddComment")]
        public async Task<IHttpActionResult> AddComment(BlogArticleComment inComment)
        {
            try
            {
                await _blogService.AddComment(inComment);
                return Ok();
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(BlogArticleComment))]
        [Route("Api/BlogArticle/DeleteComment")]
        public async Task<IHttpActionResult> DeleteComment(long  inCommentId)
        {
            try
            {
                return Ok(await _blogService.DeleteComment(inCommentId));
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPatch]
        [Route("Api/BlogArticle/UpdateComment")]
        public async Task<IHttpActionResult> UpdateComment(BlogArticleComment inComment)
        {
            try
            {
                await _blogService.UpdateComment(inComment);
                return Ok();
            }
            catch (SystemException error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
