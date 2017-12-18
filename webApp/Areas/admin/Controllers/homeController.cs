using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class homeController : Controller
    {
        //
        // GET: /admin/home/

        public ActionResult index()
        {
            return View();
        }
        //:/admin/home/ajaxhomeget
        public void ajaxhomeget()
        {
            try
            {
                Response.Clear();
                string data = BLL.Users.usersBLL.execAdmHome(Utility.adminLogin.ID);
                if (string.IsNullOrEmpty(data))
                {
                    Response.Write("null");
                }
                else
                {
                    Response.Write(data);
                }
            }
            catch (Exception m) { Response.Write(m.Message); }
            finally { Response.End(); }
        }

    }
}
