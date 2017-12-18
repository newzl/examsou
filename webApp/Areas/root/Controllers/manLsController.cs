using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    [Utility.adminAuthorize]
    public class manLsController : Controller
    {
        //
        // GET: /root/manls/

        #region view
        public ActionResult list()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(BLL.root.subksBLL.getEntity(id, "ls"));
        }
        #endregion

        #region form
        [HttpGet]
        public void listdata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("[State]=1");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    Models.subFind m = JsonConvert.DeserializeObject<Models.subFind>(wherejson);
                    if (m.sid != 0) sb.Append(" and SubjectClassID in (select id from f_getChildren(" + m.sid + "))");
                    if (m.title != "") sb.Append(" and Title like '%" + m.title.Trim() + "%'");
                    if (m.inputState != -1) sb.Append(" and inputState=" + m.inputState);
                    if (m.mindate != "") sb.Append(" and k.createTime>='" + m.mindate.Trim() + "'");
                    if (m.maxdate != "") sb.Append(" and k.createTime<='" + m.maxdate.Trim() + " 23:59:59'");
                }
                Models.paging pag = new Models.paging()
                {
                    table = "LSSubject k join subjectClass s on k.SubjectClassID=s.ID",
                    order = "k.createTime desc",
                    field = "k.id,s.name[sname],title,result,k.createTime[date],inputState[state]",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }

        [HttpPost]
        [ValidateInput(false)]
        public int save(Models.subks m)
        {
            m.creator = Utility.adminLogin.ID;
            return BLL.root.subksBLL.save(m, "ls");
        }
        //修改入库状态
        [HttpPost]
        public int instate(int id, bool state)
        {
            BLL.root.subksBLL.inState(id, state, "ls");
            return 1;
        }
        [HttpGet]
        public JsonResult getentity(int id)
        {
            return Json(BLL.root.subksBLL.getEntity(id, "ls"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
