using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class simulatePaperController : ApiController
    {
        //获得模拟考试试卷
        // GET: api/simulatepaper
        public IHttpActionResult Get(int itid)
        {
            return Json<Models.simulatePaper>(new BLL.exam.simulateBLL().getSimulatePaper(itid));
        }
        //保存模拟考试试卷
        // POST: /api/simulatepaper
        public IHttpActionResult Post(Models.simulate m)
        {
            new BLL.exam.simulateBLL().save(m);
            return Ok(1);
        }
    }
}
