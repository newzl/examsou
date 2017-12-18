using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class reportController : Controller
    {
        //
        // GET: /admin/report/
        #region 视图
        //考试情况统计
        public ActionResult examreport()
        {
            ViewBag.companyID = Utility.adminLogin.companyID;
            return View();
        }
        //注册情况统计
        public ActionResult regreport()
        {
            ViewBag.companyID = Utility.adminLogin.companyID;
            return View();
        }
        //学习情况统计
        public ActionResult learnreport()
        {
            ViewBag.companyID = Utility.adminLogin.companyID;
            return View();
        }

        //职称注册统计
        public ActionResult jobreport()
        {
            return View();
        }
        #endregion

        //考试情况统计  /admin/report/table_examreport
        public void table_examreport(examreportFindwhere m)
        {
            string whereStr = "companyID=" + Utility.adminLogin.companyID;
            if (m.bm != -1) whereStr += " and id=" + m.bm;
            if (m.ks != -1) whereStr += " and id2=" + m.ks;
            try
            {
                Response.Clear();
                Response.Write(BLL.reportBLL.examReport(m.minExam, m.maxExam, m.passScore, whereStr));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); }
        }
        public void table_leranreport(learnreportFindwhere m)
        {
            string whereStr = " 1=1";
            if (m.bm != -1) whereStr += " and id=" + m.bm;
            if (m.ks != -1) whereStr += " and id2=" + m.ks;
            try
            {
                Response.Clear();
                Response.Write(BLL.reportBLL.learnReport(m.minDate, m.maxDate, Utility.adminLogin.companyID, whereStr));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); }
        }
        public void table_regreport(int bm, int ks)
        {
            string whereStr = "companyID=" + Utility.adminLogin.companyID;
            if (bm > 0) whereStr += " and id=" + bm;
            if (ks > 0) whereStr += " and id2=" + ks;
            try
            {
                Response.Clear();
                Response.Write(BLL.reportBLL.registerReport(whereStr));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); }
        }
        //职称注册情况统计
        public void table_jobreport()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.reportBLL.reportJob(Utility.adminLogin.companyID));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); }
        }
    }
    public class examreportFindwhere
    {
        public int bm { get; set; }
        public int ks { get; set; }
        public string minExam { get; set; }
        public string maxExam { get; set; }
        public decimal passScore { get; set; }
    }
    public class learnreportFindwhere
    {
        public int bm { get; set; }
        public int ks { get; set; }
        public string minDate { get; set; }
        public string maxDate { get; set; }
    }
}
