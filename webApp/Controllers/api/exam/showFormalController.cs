using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class showFormalController : ApiController
    {
        //查看正规考试查看试卷数据
        // GET: /api/showformal
        public IHttpActionResult Get(int ansid)
        {
            return Json<Models.showFormal>(new BLL.exam.formalBLL().showFormalPaper(ansid));
        }
    }
}