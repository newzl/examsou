using System.Web.Mvc;

namespace webApp.Controllers
{
    [Utility.employeeAuthorize]
    public class myitemController : Controller
    {
        // GET: myitem
        public ActionResult index()
        {
            return View();
        }
        public ActionResult list()
        {
            return View();
        }
        public ActionResult record()
        {
            return View();
        }
    }
}