using System;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Text;
using Models;


namespace webApp.Areas.root.Controllers
{
    [Utility.adminAuthorize]
    public class teacherController : Controller
    {
        //
        // GET: /root/teacher/
        public ActionResult teacherList()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(new BLL.root.edu_teacherBLL().getTeacher(id));
        }
        [HttpGet]
        public void listdata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("isDel=0");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    teacherFin m = JsonConvert.DeserializeObject<teacherFin>(wherejson);
                    if (m.name != "") sb.Append(" and name like '%" + m.name.Trim() + "%'");
                    if (m.zc != "") sb.Append(" and zc like '%" + m.zc.Trim() + "%'");
                }
                Models.paging pag = new Models.paging()
                {
                    table = "edu_teacher",
                    order = "createTime desc",
                    field = " id, name,zc,detail,case when isHome=1 then '已显示' else '未显示' end as home,case when valid=1 then '启用' else '未启用' end as va ",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }

        [HttpPost]
        public int save(edu_teacher objedu_item)
        {
            return new BLL.root.edu_teacherBLL().save(objedu_item);
        }
        [HttpGet]
        public JsonResult getEntity(int id)
        {
            return Json(new BLL.root.edu_teacherBLL().getTeacher(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int Del(int id)
        {
            int result = new BLL.root.edu_teacherBLL().DelTeacher(id);
            return result;
        }
    }
    public class teacherFin
    {
        public string name { get; set; }
        public string zc { get; set; }
    }
}