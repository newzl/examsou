using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    [Utility.adminAuthorize]
    public class homeController : Controller
    {
        //
        // GET: /root/home/

        public ActionResult index()
        {
            return View();
        }

    }
}
