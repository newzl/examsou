using System;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using Models;
using Common;

namespace webApp.Areas.root.Controllers
{
    [Utility.adminAuthorize]
    public class keJianController : Controller
    {
        // GET: /root/keJian/
        public ActionResult list()
        {
            return View();
        }

        [HttpGet]
        public void listdata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("isnull(title,'')<>''");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    Models.subFind m = JsonConvert.DeserializeObject<Models.subFind>(wherejson);
                    //if (m.leid != "") sb.Append(" and k.typ=" + m.leid);
                    if (m.zid != "-1") sb.Append(" and cont_typ=" + m.zid);
                    if (m.title != "") sb.Append(" and title like '%" + m.title.Trim() + "%'");
                    if (m.inputState != -1) sb.Append(" and k.typ=" + m.inputState);
                    if (m.mindate != "") sb.Append(" and k.createTime>='" + m.mindate.Trim() + "'");
                    if (m.maxdate != "") sb.Append(" and k.createTime<='" + m.maxdate.Trim() + " 23:59:59'");
                }

                Models.paging pag = new Models.paging()
                {
                    table = "keJian k inner join edu_item e on k.itid=e.id left join edu_teacher t on k.teacher=t.id inner join (select type_bm,name as xxlx from c_bm where type=13)c on k.typ=c.type_bm",
                    order = "k.createTime desc",
                    field = "k.id,e.name,t.name as teachers, title,cont,xueshi,xueshi_minute,author,xxlx,cont as curl",
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

        //Models
        [HttpPost]
        [ValidateInput(false)]
        public int save(keJian objkeJian)
        {
            objkeJian.cont = DirFile.WriteInFileStream(objkeJian.cont, objkeJian.curl);
            int result = new BLL.root.keJianBLL().save(objkeJian);
            //if (result != 0 && objkeJian.curl != "0")
            //    DirFile.DeleteFile(objkeJian.curl);
            return result;

        }
        [HttpPost]
        public int Del(string curl, int id)
        {
            int result = new BLL.root.keJianBLL().Del(id);
            if (result != 0 && curl.Length != 0)
            {
                DirFile.DeleteFile(curl);
                //DirFile.DeleteFiles(curl);
            }
            return result;
        }
        [HttpGet]
        public JsonResult getentity(int id)
        {
            keJian objkeJian = new BLL.root.keJianBLL().getEntity(id);
            if (objkeJian.curl.Length > 0)
            {
                objkeJian.curl = objkeJian.cont;
                objkeJian.cont = DirFile.ReaderInFileStream(objkeJian.cont);
            }
            return Json(objkeJian, JsonRequestBehavior.AllowGet);
        }
        #region 动态获取下拉选项框的内容
        public void datajian()
        {
            try
            {
                Response.Clear();
                //string companyID = context.Request.QueryString["companyid"];
                int pid = Convert.ToInt32(Request.QueryString["pid"]);
                int cid = 1;
                string data = BLL.jbzcBLL.getSelect(cid, pid);
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