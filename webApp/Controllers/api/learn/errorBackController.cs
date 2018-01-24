using System.Web.Http;
using Utility;

namespace webApp.Controllers.api.learn
{
    /// <summary>
    /// 错题反馈
    /// </summary>
    public class errorBackController : ApiController
    {
        // GET: api/errorback
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        // 错题反馈
        // POST: api/errorback
        public IHttpActionResult Post(Models.learnErrorBack m)
        {
            m.eid = employeeLogin.eid;
            BLL.learn.learnErrorBackBLL.save(m);
            return Ok(1);
        }
    }
}
