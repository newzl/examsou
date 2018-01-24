using System.Web.Mvc;

namespace webApp.Controllers
{
    public class defaultController : Controller
    {
        // GET: default
        public ActionResult index()
        {
            return View();
        }
    }
}