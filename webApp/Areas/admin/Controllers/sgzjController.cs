using Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class sgzjController : Controller
    {

        // GET: admin/sgzj
        public ActionResult index()
        {
            return View();
        }
        public ActionResult show(int id)
        {
            ViewBag.epid = id;
            return View(BLL.examPlanBLL.show_sgzj(id));
        }
        public ActionResult update()
        {
            return View();
        }
        //  /admin/sgzj/subtable
        [HttpGet]
        public void subtable()
        {
            try
            {
                Response.Clear();
                subtableWhere w = JsonConvert.DeserializeObject<subtableWhere>(Request.QueryString["whereJson"]);
                string where = "sid in (select id from f_getChildren(" + w.sid + "))";
                if (w.stitle != "") where += " and title like '%" + w.stitle + "%'";
                if (w.stype != "all") where += " and stype='" + w.stype + "'";
                paging entity = new paging();
                entity.table = "v_findsubject";
                entity.field = "id,stype,title,sname,snav,stypet,result,total,yes";
                entity.order = "id desc";
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
        // /admin/sgzj/save
        [HttpPost]
        public int save(Models.examPlan m)
        {
            m.companyID = adminLogin.companyID;
            m.creator = adminLogin.ID;
            BLL.examPlanBLL.save_sgzj(m);
            return 1;
        }
        // /admin/sgzj/update
        [HttpPost]
        public int update(Models.examPlan m)
        {
            m.companyID = adminLogin.companyID;
            m.creator = adminLogin.ID;
            BLL.examPlanBLL.update_sgzj(m);
            return 1;
        }
        //导出试题-获得试题  GET:/admin/sgzj/export
        [HttpGet]
        public void export()
        {
            try
            {
                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(BLL.examPlanBLL.export(Convert.ToInt32(Request.QueryString["epid"]))));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
    }
    public class subtableWhere
    {
        public int sid { get; set; }
        public string stitle { get; set; }
        public string stype { get; set; }
    }
}