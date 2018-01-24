using System.Web.Http;
using Utility;

namespace webApp.Controllers.api
{
    public class homeController : ApiController
    {
        // GET: api/home
        public IHttpActionResult Get()
        {
            return Json<Models.home>(new BLL.homeBLL().execHome(employeeLogin.eid));
        }
    }
}