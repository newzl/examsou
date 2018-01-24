using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class readyExamController : ApiController
    {
        //准备考试获得json数据
        //GET:/api/readyexam
        public IHttpActionResult Get()
        {
            return Json<Models.readyExamJson>(new BLL.exam.formalBLL().readyExam(Utility.employeeLogin.eid));
        }
        [HttpGet]
        [Route("api/exam/list")]
        public IHttpActionResult list(int miid, int limit, int page)
        {
            Models.paging pag = new Models.paging()
            {
                table = "v_examScore",
                order = "examTime desc",
                field = "ansid,number,chscore,muscore,juscore,scores,isPass,usetime,examTime",
                pageSize = limit,
                pageNo = page,
                where = "miid=" + miid
            };
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                BLL.pagingBLL.runLaypage(pag,null),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
    }
}
