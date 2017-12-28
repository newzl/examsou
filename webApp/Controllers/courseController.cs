using System.Web.Mvc;
using Newtonsoft.Json;

namespace webApp.Controllers
{
    [Utility.employeeAuthorize]
    public class courseController : Controller
    {
        //[Route("course/learn/{kjid:int}/{miid:int}")]
        public ActionResult learn(int id)
        {
            ViewBag.miid = id;
            return View();
        }
    }
}