using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webApi.Controllers.learn
{
    public class mylearnController : ApiController
    {
        // GET: api/mylearn
        //我的题库table
        [HttpGet]
        public IHttpActionResult Get(string eid)
        {
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                BLL.learnSubjectBLL.getMyLearnTab(eid),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
        //开始学习
        [Route("api/mylearn/begin")]
        [HttpPost]
        public IHttpActionResult beginlearn(learnPost m)
        {
            BLL.learnSubjectBLL.beginLearn(m.lid, m.eid);
            return Ok(1);
        }
        //取消学习
        [Route("api/mylearn/cancel")]
        [HttpPost]
        public IHttpActionResult cancel(learnPost m)
        {
            BLL.learnSubjectBLL.cancel(m.lid, m.eid);
            return Ok(1);
        }
        //选择学习（添加学习）
        [HttpPost]
        public IHttpActionResult Post(saveLearnPost m)
        {
            return Ok(BLL.learnSubjectBLL.saveLearnSubject(m.eid, m.sid));
        }
    }
    public class learnPost
    {
        public string eid { get; set; }
        public int lid { get; set; }
    }
    public class saveLearnPost
    {
        public string eid { get; set; }
        public int sid { get; set; }
    }
}
