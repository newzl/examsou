using System.Web.Mvc;
using Utility;
using Newtonsoft.Json;

namespace webApp.Controllers
{
    [employeeAuthorize]
    public class examController : Controller
    {
        //考试列表
        public ActionResult list()
        {
            return View();
        }
        //准备模拟考试
        public ActionResult simulate()
        {
            Models.edu_myitem_inlearn m = new BLL.edu_myitemBLL().inlearn(employeeLogin.eid);
            if (m != null)
            {
                ViewBag.data = m.miid;
                return View();
            }
            else
            {
                return Redirect("/eduitem/search");
            }
            //return View();
        }

        #region 模拟考试
        //模拟考试试卷
        public ActionResult simulatePaper()
        {
            Models.edu_myitem_inlearn m = new BLL.edu_myitemBLL().inlearn(employeeLogin.eid);
            if (m != null)
            {
                ViewBag.data = JsonConvert.SerializeObject(new { miid = m.miid, itid = m.itid });
                return View();
            }
            else
            {
                return Redirect("/eduitem/search");
            }
        }
        //查看模拟试卷
        public ActionResult simulateShow(int id)
        {
            ViewBag.esid = id;
            return View();
        }
        #endregion

        #region 正规考试
        //正规考试试卷
        [Route("exam/formal/{miid:int}/{itid:int}")]
        public ActionResult formalPaper(int miid, int itid)
        {
            ViewBag.data = JsonConvert.SerializeObject(new { miid = miid, itid = itid });
            return View();
        }
        //查看正规考试试卷
        public ActionResult formalShow(int id)
        {
            ViewBag.id = id;
            return View();
        }
        #endregion

    }
}