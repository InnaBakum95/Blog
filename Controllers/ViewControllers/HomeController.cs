using System.Web.Mvc;

namespace NewBlogAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult Authorize()
        {
            return View();
        }
    }
}
