using System.Net.Http;
using System.Text;
using System.Web.Http;
using Models;
namespace webApi.Controllers.profile
{
    public class selesubjectController : ApiController
    {
        // GET: api/selesubject
        [HttpGet]
        public IHttpActionResult Get(string eid)
        {
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                BLL.subjectClassBLL.seleSubject(eid),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
        //题库详情
        // GET: api/selesubject/detail
        [Route("api/selesubject/detail")]
        [HttpGet]
        public IHttpActionResult detail(int id, string eid)
        {
            return Json<subjectDetail>(BLL.subjectClassBLL.detailEntity(id, eid));
        }
    }
}
