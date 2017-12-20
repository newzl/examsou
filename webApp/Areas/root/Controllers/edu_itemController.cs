using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    public class edu_itemController : Controller
    {
        //
        // GET: /root/edu_item/
        public ActionResult edu_itemList()
        {
            return View();
        }

        #region 动态获取下拉框
        //获取项目
        public void DataTyp()
        {
            try
            {
                Response.Clear();
                string data = BLL.root.edu_itemBLL.GetTyp();
                if (string.IsNullOrEmpty(data))
                    Response.Write("null");
                else
                    Response.Write(data);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); };
        }

        //获取老师
        public void dataTeacher()
        {
            try
            {
                Response.Clear();
                string data = BLL.root.edu_teacherBLL.getTeacher();
                if (string.IsNullOrEmpty(data))
                {
                    Response.Write("null");
                }
                else
                    Response.Write(data);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { Response.End(); };
        }
        //获取试题集
        public void datastk()
        {
            try
            {
                Response.Clear();
                string data = BLL.subjectClassBLL.Get_scid();
                if (string.IsNullOrEmpty(data))
                {
                    Response.Write("null");
                }
                else
                {
                    Response.Write(data);
                }
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