using System;
using System.Text;
using System.Web.Mvc;
using Utility;

namespace webApp.Controllers
{
    [employeeAuthorize]
    public class feedbackController : Controller
    {
        // 意见反馈
        // GET: /feedback/

        public ActionResult index()
        {
            return View();
        }
        #region form
        //保存意见反馈  /feedback/save
        [HttpPost]
        [ValidateInput(false)]
        public int save(string content)
        {
            BLL.feedbackBLL.saveFeedback(employeeLogin.eid, content);
            return 1;
        }
        [HttpGet]
        // 获得意见反馈
        public void tabledata() {
            try
            {
                Response.Clear();
                string sb = "display=1";
                Models.paging pag = new Models.paging()
                {
                    table = "v_feedback",
                    order = "replyTime desc,[date] desc",
                    field = "photo,name,[date],content,isdispose,reply",
                    pageSize = 10,
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb
                };
                Response.Write(BLL.pagingBLL.runpage(pag, "yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        #endregion

    }
}
