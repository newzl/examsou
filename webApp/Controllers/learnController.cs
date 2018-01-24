using System.Web.Mvc;
using Newtonsoft.Json;
using Utility;

namespace webApp.learn.Controllers
{
    [employeeAuthorize]
    public class learnController : Controller
    {
        //我的学习
        //public ActionResult __index()
        //{
        //    Models.learnIndex m = BLL.learnSubjectBLL.learnIndex(Utility.employeeLogin.eid);
        //    if (m != null)
        //    {
        //        ViewBag.lid = m.lid;
        //        ViewBag.sid = m.sid;
        //        ViewBag.sname = m.sname;
        //        ViewBag.data = JsonConvert.SerializeObject(m);
        //    }
        //    else
        //    {
        //        ViewBag.data = "null";
        //    }
        //    return View();
        //}
        //章节练习 /learn/chapter
        public ActionResult chapter()
        {
            return View();
        }
        //顺序练习
        [Route("practise/{miid:int}/{scid:int}/{stype}")]
        public ActionResult learnsx(int miid, int scid, string stype = "ch")
        {
            Models.learnInfo m = BLL.learn.learnCommonBLL.getLearnBaseInfo(miid, scid, stype);
            if (m != null)
            {
                m.miid = miid;
                m.scid = scid;
                m.stype = stype;
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
                return Redirect("/learn/chapter");
            }
        }
    }
}
