using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webApp.Controllers.api
{
    public class chapterController : ApiController
    {
        //获得章节练习（题库练习章节）
        // GET: api/chapter
        public IHttpActionResult Get(int scid)
        {
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                new BLL.learnSubjectBLL().getChapter(Utility.employeeLogin.eid, scid),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
    }
}
