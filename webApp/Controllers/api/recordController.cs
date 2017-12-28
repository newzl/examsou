using Models;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace webApp.Controllers.api
{
    public class recordController : ApiController
    {
        //获得正在学习课件list  --用于/course/learn/
        // GET: api/record
        public IHttpActionResult Get(int miid)
        {
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                new BLL.edu_recordBLL().getCourse(miid),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
        // 保存学习记录
        // POST: api/record
        public IHttpActionResult Post(edu_record m)
        {
            new BLL.edu_recordBLL().saveRecord(m);
            return Ok(1);
        }
    }
}
