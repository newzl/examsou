using System;
using System.Web.Mvc;

namespace webApp.Controllers
{
    public class attemptController : Controller
    {
        //
        // GET: /attempt/

        public ActionResult examtest(int id)
        {
            string title = BLL.subjectClassBLL.getName(id);
            if (string.IsNullOrEmpty(title))
            {
                return Redirect("/");
            }
            else
            {
                ViewBag.subTitle = title;
                ViewBag.sid = id;
                return View();
            }

        }
        //:/attempt/getexamtest
        [HttpGet]
        public void getexamtest()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.exam.examTestBLL.getExamTest(Convert.ToInt32(Request.QueryString["sid"])));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }

    }
}
