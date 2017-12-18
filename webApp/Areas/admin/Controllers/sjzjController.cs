using Models;
using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class sjzjController : Controller
    {
        // GET: admin/sjzj
        public ActionResult index()
        {
            return View();
        }
        public ActionResult show(int id)
        {
            return View(BLL.examPlanBLL.show_sjzj(id));
        }
        public ActionResult update()
        {
            return View();
        }
        [HttpGet]
        public void subtable()
        {
            try
            {
                Response.Clear();
                string where = "valid=1 and chs+mus+jus>0 and pid=" + Convert.ToInt32(Request.QueryString["sid"]);
                paging entity = new paging();
                entity.table = "subjectClass";
                entity.field = "id,name,chs,mus,jus";
                entity.order = "id";
                entity.where = where;
                entity.pageSize = Convert.ToInt32(Request.QueryString["limit"]);
                entity.pageNo = Convert.ToInt32(Request.QueryString["page"]);
                Response.Write(BLL.pagingBLL.runLaypage(entity));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        // /admin/sjzj/save
        [HttpPost]
        public int save(Models.examPlan m)
        {
            m.companyID = adminLogin.companyID;
            m.creator = adminLogin.ID;
            BLL.examPlanBLL.save_sjzj(m);
            return 1;
        }
        // /admin/sgzj/update
        [HttpPost]
        public int update(Models.examPlan m)
        {
            m.companyID = adminLogin.companyID;
            m.creator = adminLogin.ID;
            BLL.examPlanBLL.update_sjzj(m);
            return 1;
        }
    }
}