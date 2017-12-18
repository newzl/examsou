using System.Web.Mvc;
using Utility;

namespace webApp.Controllers
{
    public class homeController : Controller
    {
        //
        // GET: /home/

        public ActionResult index()
        {
            ViewBag.isLogin = Utility.employeeLogin.isLogin.ToString();
            return View();
        }

    }
}
