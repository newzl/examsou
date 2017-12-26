using System.Net.Http;
using System.Text;
using System.Web.Http;
using Utility;

namespace webApp.Controllers.api
{
    public class myitemController : ApiController
    {
        //我的继教项目列表
        // GET: api/myitem
        public IHttpActionResult Get()
        {
            Models.paging pag = new Models.paging()
            {
                table = "edu_myitem a left join edu_item b on a.itid=b.id left join vp_itemTyp c on b.typ=c.id",
                order = "a.createTime desc",
                field = "a.id,a.itid,b.name,c.name[typ],nian,xf,isSucceed,a.createTime,a.inlearn",
                pageSize = 50,
                pageNo = 1,
                where = "a.eid='" + employeeLogin.eid + "'"
            };
            return ResponseMessage(
            new HttpResponseMessage
            {
                Content = new StringContent(
                BLL.pagingBLL.runLaypage(pag),
                Encoding.GetEncoding("UTF-8"), "application/json")
            });
        }
        //开始学习
        // POST: api/myitem
        public IHttpActionResult Post(myItemPost m)
        {
            new BLL.edu_myitemBLL().beginLearn(m.miid, employeeLogin.eid);
            return Ok(1);
        }
        // 取消学习
        // DELETE: api/myitem/5
        public IHttpActionResult Delete(myItemPost m)
        {
            new BLL.edu_myitemBLL().delete(m.miid);
            return Ok(1);
        }
    }
    public class myItemPost
    {
        public int miid { get; set; }
    }
}
