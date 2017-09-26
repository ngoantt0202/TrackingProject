using System.Web.Mvc;

namespace TaskTracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string token = "";
            if (Session["accessToken"] != null)
            {
                token = (string)Session["accessToken"];
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.Title = "Home Page";
      
            return View();
        }
    }
}
