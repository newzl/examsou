using System;
using System.Text;
using System.Web.Mvc;
using Utility;
using Newtonsoft.Json;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class departmentController : Controller
    {
        //
        // GET: /admin/department/

        public ActionResult list()
        {
            return View();
        }
        #region form
        [HttpGet]// /admin/department/tabledataks
        public void tabledataks()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("companyID=" + adminLogin.companyID.ToString());

                string bmid = Request.QueryString["bmid"];
                if (!string.IsNullOrEmpty(bmid))
                {
                    if (Convert.ToInt32(bmid) != -1) sb.Append(" and bmID=" + bmid);
                }
                Models.paging pag = new Models.paging()
                {
                    table = "v_bmks",
                    order = "createTime desc",
                    field = "ksID[id],bm,ks,must,number,createTime,isExam,valid",
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
        [HttpGet]// /admin/department/tabledatabm
        public void tabledatabm()
        {
            try
            {
                Response.Clear();
                string wherestr="[type]=1 and companyID=" + adminLogin.companyID.ToString();

                Models.paging pag = new Models.paging()
                {
                    table = "(select * from department where [type]=1) a left join(select bmID,COUNT(*)[kss] from v_bmks group by bmID) b on a.ID=b.bmID",
                    order = "createTime desc",
                    field = "id,name,isnull(kss,0)[kss],createTime,valid",
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
        /// 保存科室
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>/admin/department/saveks
        [HttpPost]
        public JsonResult saveks(Models.department m)
        {
            m.companyID = Utility.adminLogin.companyID;
            m.type = 2;
            BLL.departmentBLL.save(m);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult savebm(Models.department m)
        {
            m.companyID = Utility.adminLogin.companyID;
            m.type = 1;
            m.isExam = true;
            m.must = 0;
            m.pid = 0;
            BLL.departmentBLL.save(m);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>/admin/department/delete
        [HttpPost]
        public JsonResult deleteks(int id)
        {
            BLL.departmentBLL.delete(id);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult deletebm(int id)
        {
            BLL.departmentBLL.deletebm(id);
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
                Response.Write(JsonConvert.SerializeObject(BLL.departmentBLL.getEntity(id)));
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
