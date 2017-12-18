using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Utility;

namespace webApp.learn.Controllers
{
    [employeeAuthorize]
    public class learnController : Controller
    {
        //我的学习
        public ActionResult index()
        {
            Models.learnIndex m = BLL.learnSubjectBLL.learnIndex(Utility.employeeLogin.eid);
            if (m != null)
            {
                ViewBag.lid = m.lid;
                ViewBag.sid = m.sid;
                ViewBag.sname = m.sname;
                ViewBag.data = JsonConvert.SerializeObject(m);
            }
            else
            {
                ViewBag.data = "null";
            }
            return View();
        }
        //章节练习 /learn/chapter
        public ActionResult chapter()
        {
            Models.inlearn m = BLL.learnSubjectBLL.getInlearn(Utility.employeeLogin.eid);
            if (m != null)
            {
                ViewBag.sname = m.sname;
                ViewBag.data = JsonConvert.SerializeObject(m);
                return View();
            }
            else
            {
                return Redirect("/subject");
            }
        }
        //学习记录
        public ActionResult record()
        {
            return View();
        }

        //顺序
        public ActionResult learnsx(int lid, int sid, string stype = "ch")
        {
            Models.learnInfo m = BLL.learn.learnCommonBLL.getLearnBaseInfo(lid, sid, stype);
            if (m != null)
            {
                m.lid = lid;
                m.sid = sid;
                m.stype = stype;
                ViewBag.sname = m.sname;
                ViewBag.data = JsonConvert.SerializeObject(m);
                string str = null;
                switch (stype)
                {
                    case "ch":
                        str = "exerch";
                        break;
                    case "mu":
                        str = "exermu";
                        break;
                    case "ju":
                        str = "exerju";
                        break;
                    default:
                        str = "exerks";
                        break;
                }
                return View("~/Views/learn/" + str + ".cshtml");
            }
            else
            {
                return Redirect("/learn");
            }
        }

        #region form
        [HttpGet]//  /learn/getchapter
        public void getchapter(int lid, int sid)
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.learnSubjectBLL.getChapter(lid, sid));
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
