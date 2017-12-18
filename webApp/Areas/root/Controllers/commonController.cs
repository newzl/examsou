using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    public class commonController : Controller
    {
        //
        // GET: /root/common/

        public ActionResult findTree()
        {
            return View();
        }
        public ActionResult selTree()
        {
            return View();
        }
        public ActionResult edit_kindeditor()
        {
            return View();
        }
        public ActionResult edit_xheditor()
        {
            return View();
        }
    }
}
