using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class showSimulateController : ApiController
    {
        //查看模拟考试试卷
        // GET: api/showsimulate
        public IHttpActionResult Get(int esid)
        {
            return Json<Models.showSimulate>(new BLL.exam.simulateBLL().showSimulatePaper(esid));
        }
    }
}