using System.Web.Http;
using Models;

namespace webApi.Controllers.learn
{
    public class learnsubjecController : ApiController
    {
        //在线学习首页
        // GET: api/learnsubjec
        [HttpGet]
        public IHttpActionResult Get(string eid)
        {
            learnIndex m = BLL.learnSubjectBLL.learnIndex(eid);
            return Json<learnIndex>(m);
        }
    }
}
