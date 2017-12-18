using System;
using System.Web.Mvc;
using Utility;
using Newtonsoft.Json;
namespace webApp.Controllers
{
    [employeeAuthorize]
    public class profileController : Controller
    {
        //
        // GET: /profile/
        #region view
        //基本信息
        public ActionResult personal()
        {
            return View();
        }
        //职业信息
        public ActionResult profession()
        {
            return View();
        }
        public ActionResult bkjz(int id)
        {
            ViewBag.companyID = id;
            return View();
        }
        //个人中心
        public ActionResult core()
        {
            return View();
        }
        //我的消息
        public ActionResult messages()
        {
            return View();
        }
        //修改密码
        public ActionResult changepwd()
        {
            return View();
        }
        //上传证件
        public ActionResult uploadzj()
        {
            return View();
        }
        #endregion

        #region form
        /// <summary>
        /// 个人中心获得数据
        /// </summary>
        [HttpGet]
        public void datacore()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.account.employeeBLL.profileCore(employeeLogin.eid));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //判断是否可以认证
        [HttpGet]
        public int isapply()
        {
            return BLL.account.employeeBLL.isApply(employeeLogin.eid);
        }
        //修改密码
        [HttpPost]
        public int changepwdform(string oldpwd, string pwdc)
        {
            return BLL.account.employeeBLL.changePwd(employeeLogin.eid, Common.Encrypt.md5(oldpwd), Common.Encrypt.md5(pwdc));
        }
        //基本资料获得数据 /profile/datapersonal
        [HttpGet]
        public void datapersonal()
        {
            try
            {
                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(BLL.account.employeeBLL.getPersonalEntity(employeeLogin.eid)));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //获得工作信息entity  GET:/profile/professionentity
        [HttpGet]
        public void professionEntity()
        {
            try
            {
                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(BLL.account.employeeBLL.getProfessionEntity(employeeLogin.eid)));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }

        //基本资料获得部门科室，级别职称json  /profile/databkjz
        [HttpGet]
        public void databkjz()
        {
            try
            {
                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(BLL.account.employeeBLL.getBkjzEntity(employeeLogin.eid)));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //保存基本资料  、/profile/savepersonal
        [HttpPost]
        public JsonResult savepersonal(Models.employeePersonal m)
        {
            m.eid = employeeLogin.eid;
            int res = BLL.account.employeeBLL.saveEmployee(m);
            if (res == 1)
            {
                return Json(new
                {
                    msg = 1,
                    info = BLL.account.employeeBLL.getCookieEntity(m.eid)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    msg = res
                }, JsonRequestBehavior.AllowGet);
            }
        }
        // 保存工作信息 POST：/profile/saveprofession
        [HttpPost]
        public int saveProfession(Models.employeeProfession m)
        {
            m.eid = employeeLogin.eid;
            return BLL.account.employeeBLL.saveProfession(m);
        }
        public int savebkjz(Models.employee_bkjz m)
        {
            m.eid = employeeLogin.eid;
            return BLL.account.employeeBLL.saveBkjz(m);
        }
        //获得证件照的路径 /profile/getphoto
        public JsonResult getphoto()
        {
            string data = BLL.account.employeeBLL.getPhoto(employeeLogin.eid);
            if (string.IsNullOrEmpty(data))
            {
                return Json(new { msg = 0, pic = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = 1, pic = data }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
