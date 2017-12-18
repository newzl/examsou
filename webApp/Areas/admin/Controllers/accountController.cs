using Models;
using System.Web.Mvc;

namespace webApp.Areas.admin.Controllers
{
    public class accountController : Controller
    {
        //
        // GET: /admin/account/

        public ActionResult login()
        {
            if (Utility.adminLogin.isLogin)
            {
                if (Utility.adminLogin.companyID > 0)
                {
                    return Redirect("/admin");
                }
                else
                {
                    return Redirect("/root");
                }
            }
            else return View();
        }
        //退出
        public ActionResult quitlogin()
        {
            Utility.adminLogin.removeLogin();
            return Redirect("/admin/account/login");
        }
        //登录
        [HttpPost]
        public JsonResult loginform(string account, string pwd, string code)
        {
            if (Session[Utility.globalValue.SESSION_VERIFY_CODE].ToString().Equals(code.ToUpper()))
            {
                users entity = BLL.Users.usersBLL.login(account, Common.Encrypt.md5(pwd));
                if (entity != null)
                {
                    Utility.adminLogin.setLoginSession(entity);
                    if (Utility.adminLogin.isLogin)
                    {
                        BLL.Users.usersBLL.lastLoginTime(entity.ID);
                        return Json(new { state = 1, type = entity.type }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { state = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { state = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { state = -1 }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
