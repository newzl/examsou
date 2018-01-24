using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webApp.Controllers.api.exam
{
    public class simulateRecordController : ApiController
    {
        // GET: api/simulateRecord
        public IHttpActionResult Get(int miid, int limit, int page)
        {
            Models.paging pag = new Models.paging()
            {
                table = "examSimulate",
                order = "createtime desc",
                field = "id,score,usetime,createtime",
                pageSize = limit,
                pageNo = page,
                where = "miid=" + miid
            };
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                BLL.pagingBLL.runLaypage(pag, null),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
        // 删除模拟考试
        // DELETE: api/simulateRecord/5
        public IHttpActionResult Delete(int id)
        {
            new BLL.exam.simulateBLL().delete(id);
            return Ok(1);
        }
    }
}
