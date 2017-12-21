using System;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using Models;
using Common;

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

        [HttpGet]
        public void ListData()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("isDel=0");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    Models.subFind m = JsonConvert.DeserializeObject<Models.subFind>(wherejson);
                    if (m.leid != "") sb.Append(" and bh='" + m.leid + "'");
                    //if (m.zid != "") sb.Append(" and sid=" + m.zid);
                    if (m.title != "") sb.Append(" and name like '%" + m.title.Trim() + "%'");
                    //if (m.inputState != -1) sb.Append(" and typ=" + m.inputState);
                    if (m.mindate != "") sb.Append(" and createTime>='" + m.mindate.Trim() + "'");
                    if (m.maxdate != "") sb.Append(" and createTime<='" + m.maxdate.Trim() + " 23:59:59'");
                }

                Models.paging pag = new Models.paging()
                {
                    table = "edu_item",
                    order = "createTime desc",
                    field = " id,bh,name,pic,typ,xf,fzr,fzdw,detail,scid,case when isHome=1 then '已显示' else '未显示' end as home,case when valid=1 then '启用' else '未启用' end as va",
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
        public int Save(edu_item objedu_item)
        {

            int result = new BLL.root.edu_itemBLL().save(objedu_item);
            return result;

        }

        [HttpGet]
        public JsonResult GetList(int id)
        {
            edu_item objList = new BLL.root.edu_itemBLL().ByIdEduItem(id);
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Del(int id)
        {
            int result = new BLL.root.edu_itemBLL().DelEduItem(id);
            return result;
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
        //public void dataTeacher()
        //{
        //    try
        //    {
        //        Response.Clear();
        //        string data = BLL.root.edu_teacherBLL.getTeacher();
        //        if (string.IsNullOrEmpty(data))
        //        {
        //            Response.Write("null");
        //        }
        //        else
        //            Response.Write(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    finally { Response.End(); };
        //}
        //获取试题集
        public void datastk()
        {
            try
            {
                Response.Clear();
                int sid = Convert.ToInt32(Request.QueryString["pid"]);
                string data = BLL.subjectClassBLL.GetScid(sid);
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
        public void datastks()
        {
            try
            {
                Response.Clear();
                string data = BLL.subjectClassBLL.GetLei();
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