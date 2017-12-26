using System.Web.Mvc;
using Newtonsoft.Json;

namespace webApp.Controllers
{
    [Utility.employeeAuthorize]
    public class courseController : Controller
    {
        [Route("course/learn/{kjid:int}/{miid:int}")]
        public ActionResult learn(int kjid, int miid)
        {
            ViewBag.data = JsonConvert.SerializeObject(new
            {
                miid = miid,
                kjin = kjid
            });
            return View();
        }
    }
}