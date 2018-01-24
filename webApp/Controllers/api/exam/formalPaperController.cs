using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class formalPaperController : ApiController
    {
        //获得正规考试试卷
        // GET: /api/formalpaper
        public IHttpActionResult Get(int itid)
        {
            return Json<Models.formalPaper>(new BLL.exam.formalBLL().getFormalPaper(itid));
        }
        //保存试卷
        // POST: /api/formalpaper
        public IHttpActionResult Post(Models.formalExam m)
        {
            new BLL.exam.formalBLL().save(m);
            return Ok(1);
        }
    }
}
