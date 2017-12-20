using System.Web.Mvc;

namespace webApp.Controllers
{
    public class defaultController : Controller
    {
        // GET: default
        public ActionResult index()
        {
            ViewBag.isLogin = Utility.employeeLogin.isLogin;
            return View();
        }
    }
}