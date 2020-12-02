using System.Web.Mvc;

namespace MVCDashboard.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginData model) {
            HttpContext.Session["CurrentUser"] = model.UserName;
            return View("Dashboard");
        }
    }
}