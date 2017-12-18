using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;
using Utility;
namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class employeemsgController : Controller
    {
        //
        // GET: /admin/employeemsg/
        #region view
        public ActionResult list()
        {
            ViewBag.companyID = adminLogin.companyID;
            return View();
        }

        public ActionResult authorizes()
        {
            ViewBag.companyID = adminLogin.companyID;
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(BLL.account.employeeBLL.getDetail(id));
        }
        #endregion

        #region form
        [HttpGet]
        public void tabledata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("valid=1 and companyID=" + adminLogin.companyID.ToString());
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    employeeListFindwhere m = JsonConvert.DeserializeObject<employeeListFindwhere>(wherejson);
                    if (m.ename != "") sb.Append(" and name like '%" + m.ename.Trim() + "%'");
                    if (m.sex != -1) sb.Append(" and sexID=" + m.sex);
                    if (m.idcard != "") sb.Append(" and idcard like '%" + m.idcard.Trim() + "%'");
                    if (m.bm != -1) sb.Append(" and departmentID=" + m.bm);
                    if (m.ks != -1) sb.Append(" and officesID=" + m.ks);
                    if (m.jb != -1) sb.Append(" and levelID=" + m.jb);
                    if (m.zc != -1) sb.Append(" and jobInfoID=" + m.zc);
                    if (m.minreg != "") sb.Append(" and registerTime>='" + m.minreg.Trim() + "'");
                    if (m.maxreg != "") sb.Append(" and registerTime<='" + m.maxreg.Trim() + " 23:59:59'");
                    if (m.state != -1) sb.Append(" and state=" + m.state);
                }
                Models.paging pag = new Models.paging()
                {
                    table = "v_employeeList",
                    order = "applyTime desc,registerTime desc",
                    field = "id,name,sex,idcard,phone,registerTime,department,offices,[level],jobInfo,[state]",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag, null));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //修改认证状态  /admin/employeemsg/updatestate
        public int updatestate(int state, int id)
        {
            BLL.account.employeeBLL.updateState(state, id);
            return 1;
        }
        [HttpGet]
        public void listdata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("valid=1 and companyID=" + adminLogin.companyID.ToString());
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    employeeListDataFindwhere m = JsonConvert.DeserializeObject<employeeListDataFindwhere>(wherejson);
                    if (m.ename != "") sb.Append(" and name like '%" + m.ename.Trim() + "%'");
                    if (m.sex != -1) sb.Append(" and sexID=" + m.sex);
                    if (m.idcard != "") sb.Append(" and idcard like '%" + m.idcard.Trim() + "%'");
                    if (m.phone != "") sb.Append(" and phone like '%" + m.phone.Trim() + "%'");
                    if (m.bm != -1) sb.Append(" and departmentID=" + m.bm);
                    if (m.ks != -1) sb.Append(" and officesID=" + m.ks);
                    if (m.jb != -1) sb.Append(" and levelID=" + m.jb);
                    if (m.zc != -1) sb.Append(" and jobInfoID=" + m.zc);
                    if (m.minlogin != "") sb.Append(" and lastLoginTime>='" + m.minlogin.Trim() + "'");
                    if (m.maxlogin != "") sb.Append(" and lastLoginTime<='" + m.maxlogin.Trim() + " 23:59:59'");
                    if (m.minreg != "") sb.Append(" and registerTime>='" + m.minreg.Trim() + "'");
                    if (m.maxreg != "") sb.Append(" and registerTime<='" + m.maxreg.Trim() + " 23:59:59'");
                    if (m.minlearns != "") sb.Append(" and learns>=" + m.minlearns);
                    if (m.maxlearns != "") sb.Append(" and learns<=" + m.maxlearns);
                    if (m.minexam != "") sb.Append(" and exams>=" + m.minexam);
                    if (m.maxexam != "") sb.Append(" and exams<=" + m.maxexam);
                    if (m.state != -1) sb.Append(" and state=" + m.state);
                }
                Models.paging pag = new Models.paging()
                {
                    table = "v_employeeList",
                    order = "registerTime desc",
                    field = "id,name,sex,idcard,nation,degree,zzmm,department,offices,[level],jobInfo,[state],logins,learns,exams,account,phone,email,lastLoginTime,registerTime",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag, null));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        #endregion
    }
    public class employeeListFindwhere
    {
        public string ename { get; set; }
        public int sex { get; set; }
        public string idcard { get; set; }
        public int bm { get; set; }
        public int ks { get; set; }
        public int jb { get; set; }
        public int zc { get; set; }
        public string minreg { get; set; }
        public string maxreg { get; set; }
        public int state { get; set; }
    }
    public class employeeListDataFindwhere : employeeListFindwhere
    {
        public string phone { get; set; }
        public string maxlogin { get; set; }
        public string minlogin { get; set; }
        public string minlearns { get; set; }
        public string maxlearns { get; set; }
        public string minexam { get; set; }
        public string maxexam { get; set; }
    }
}
