using System.Web.Http;
using Utility;

namespace webApp.Controllers.api
{
    /// <summary>
    /// 我的学习首页获得json数据
    /// </summary>
    public class eduindexController : ApiController
    {
        // GET: api/eduindex
        public IHttpActionResult Get()
        {
            return Json<Models.edu_index>(new BLL.edu_myitemBLL().edu_index(employeeLogin.eid));
        }
    }
}