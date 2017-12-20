using System;
using System.Web.Mvc;
using Models;
using System.Web;
using Utility;
namespace webApp.Controllers
{
    public class accountController : Controller
    {
        #region 视图
        //登录
        public ActionResult login()
        {
            if (employeeLogin.isLogin) employeeLogin.removeLogin();
            return View();
        }
        //退出
        public ActionResult quitlogin()
        {
            employeeLogin.removeLogin();
            return View();
        }
        //注册
        public ActionResult reg()
        {
            return View();
        }
        //其他方式注册
        public ActionResult otherreg()
        {
            return View();
        }
        //找回密码
        public ActionResult backpwd()
        {
            return View();
        }
        //验证码--cookie形式
        public ActionResult verification()
        {
            Common.ValidateCode validate = new Common.ValidateCode();
            string code = validate.CreateValidateCode(4).ToUpper();
            Response.Cookies.Add(new HttpCookie(globalValue.COOKIE_VERIFY_CODE, code));
            Byte[] buffer = validate.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion

        #region 表单
        //登录
        [HttpPost]
        public JsonResult loginform(loginfield m)
        {
            if (Request.Cookies[globalValue.COOKIE_VERIFY_CODE].Value.Equals(m.code.ToUpper()))
            {
                employeeLoginInfo employee = BLL.account.employeeBLL.login(m.account, Common.Encrypt.md5(m.pwd));
                if (employee != null)
                {
                    employeeLogin.setLogin(employee.eid,m.auto);//设置cookie
                    return Json(new
                    {
                        state = 1,
                        info= new {
                            name = employee.name,
                            photo = employee.photo
                        }
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
