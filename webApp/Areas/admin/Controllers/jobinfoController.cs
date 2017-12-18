using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class jobinfoController : Controller
    {
        //
        // GET: /admin/jobinfo/

        public ActionResult list()
        {
            return View();
        }
        public void tabledata()
        {
            try
            {
                Response.Clear();
                string wherestr = "companyID=" + adminLogin.companyID.ToString();
                string jbid = Request.QueryString["jbid"];
                if (!string.IsNullOrEmpty(jbid))
                {
                    if (Convert.ToInt32(jbid) != -1) wherestr += " and levelID=" + jbid;
                }
                Models.paging pag = new Models.paging()
                {
                    table = "JobInfo a left join subjectLevel b on a.levelID=b.ID",
                    order = "createTime desc",
                    field = "a.id,b.name[jb],jobName[name],createTime,a.valid",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = wherestr
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>/admin/department/saveks
        [HttpPost]
        public JsonResult save(Models.jobinfo m)
        {
            m.companyID = Utility.adminLogin.companyID;
            BLL.jbzcBLL.save(m);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>/admin/department/delete
        [HttpPost]
        public JsonResult deletezc(int id)
        {
            BLL.jbzcBLL.deletezc(id);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        //获得科室实体
        [HttpGet]
        public void getentity(int id)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(BLL.jbzcBLL.getEntity(id)));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
    }
}
