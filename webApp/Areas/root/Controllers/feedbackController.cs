using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    [Utility.adminAuthorize]
    public class feedbackController : Controller
    {
        //
        // GET: /root/feedback/

        public ActionResult list()
        {
            return View();
        }

    }
}
