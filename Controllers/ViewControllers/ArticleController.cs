using NewBlogAPI.Components.BlogArticleComponents;
using NewBlogAPI.Components.BlogArticleComponents.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Web.Mvc;

namespace NewBlogAPI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IBlogArticleService _blogService;

        public ArticleController(IBlogArticleService blogService)
        {
            _blogService = blogService;
        }
        // Post: Article
        [System.Web.Mvc.Authorize]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public ActionResult Article(BlogArticle blogArticle)
        {

#if FILES_TO_FILESYSTEM
            _blogService.CreateArticle(blogArticle, HttpContext.Server.MapPath("~/Files"));
#else
            _blogService.CreateArticle(blogArticle);
#endif
            return View();
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Create()
        {
            return View("Article");
        }


    }
}