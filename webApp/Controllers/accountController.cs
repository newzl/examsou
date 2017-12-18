using System;
using System.Web.Mvc;
using Models;

namespace webApp.Controllers
{
    public class accountController : Controller
    {
        #region 视图
        public ActionResult login()
        {
            if (Utility.employeeLogin.isLogin) return RedirectToAction("index", "learn");
            else return View();
        }
        //退出
        public ActionResult quitlogin()
        {
            Utility.employeeLogin.removeLogin();
            return View();
        }
        public ActionResult reg()
        {
            return View();
        }
        public ActionResult otherreg()
        {
            return View();
        }
        public ActionResult backpwd()
        {
            return View();
        }
        public ActionResult verification()
        {
            Common.ValidateCode validate = new Common.ValidateCode();
            string code = validate.CreateValidateCode(4).ToUpper();
            Session[Utility.globalValue.SESSION_VERIFY_CODE] = code;
            Byte[] buffer = validate.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 表单
        //登录
        [HttpPost]
        public JsonResult loginform(string account, string pwd, string code)
        {
            if (Session[Utility.globalValue.SESSION_VERIFY_CODE].ToString().Equals(code.ToUpper()))
            {
                employeeLoginInfo employee = BLL.account.employeeBLL.login(account, Common.Encrypt.md5(pwd));
                if (employee != null)
                {
                    employeeSession sessionObj = new employeeSession{
                        eid = employee.eid
                    };
                    Utility.employeeLogin.setLoginSession(sessionObj);//设置Session
                    return Json(new
                    {
                        state = 1,
                        name = employee.name,
                        photo = employee.photo
                    }, JsonRequestBehavior.AllowGet);
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

        //手机获得动态码
        [HttpPost]
        public int getphonecode(string phone)
        {
            return BLL.account.employeeBLL.sendPhoneCode(phone);
        }
        //邮箱获得动态码
        [HttpPost]
        public int getemailcode(string email)
        {
            return BLL.account.employeeBLL.sendEmailCode(email);
        }
        //手机或邮箱获得动态码
        [HttpPost]
        public int getbackpwdcode(string account)
        {
            return BLL.account.employeeBLL.sendBackPwdCode(account);
        }
        //手机号注册
        [HttpPost]
        public int regphone(string phone, string pwdc, string code)
        {
            return BLL.account.employeeBLL.regPhone(phone, Common.Encrypt.md5(pwdc), code);
        }
        //邮箱号注册
        [HttpPost]
        public int regemail(string account, string email, string pwdc, string code)
        {
            return BLL.account.employeeBLL.regEmail(account, email, Common.Encrypt.md5(pwdc), code);
        }
        //重置密码
        [HttpPost]
        public int resetpwd(string account, string pwdc, string code)
        {
            return BLL.account.employeeBLL.backPwd(account, Common.Encrypt.md5(pwdc), code);
        }
        #endregion
    }
}
