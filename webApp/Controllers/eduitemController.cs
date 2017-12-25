using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;

namespace webApp.Controllers
{
    public class eduitemController : Controller
    {
        #region view
        // GET: eduitem
        public ActionResult index()
        {
            return View();
        }
        public ActionResult search()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(new BLL.root.edu_itemBLL().detail(id));
        }
        public ActionResult myitem()
        {
            return View();
        }
        #endregion

        //GET:/eduitem/searchlist
        [HttpGet]
        public void searchList()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("isDel=0 and runDate<=GETDATE()");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    eduItemSearchFind m = JsonConvert.DeserializeObject<eduItemSearchFind>(wherejson);
                    if (m.scid >= 0) sb.Append(" and scid =" + m.scid);
                    if (m.name != "") sb.Append("and (a.name like '%" + m.name.Trim() + "%' or bh like '%" + m.name.Trim() + "%')");
                    if (m.typ >= 0) sb.Append(" and typ=" + m.typ);
                }
                Models.paging pag = new Models.paging()
                {
                    table = "edu_item a left join vp_itemTyp b on a.typ=b.id left join (select id,itid from edu_myitem where nian=DATEPART(Year,GETDATE())and eid='" + Utility.employeeLogin.eid + "') c on a.id=c.itid",
                    order = "a.createTime desc",
                    field = "a.id,pic,a.name,b.name[itemTyp],xf,learns,bh,fzdw,c.id[miid]",
                    pageSize = 10,
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runpage(pag));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //我的继教项目list
        //GET:/eduitem/myitemlist
        [HttpGet]
        public void myitemList()
        {
            try
            {
                Response.Clear();
                Models.paging pag = new Models.paging()
                {
                    table = "edu_myitem a left join edu_item b on a.itid=b.id left join vp_itemTyp c on b.typ=c.id",
                    order = "a.createTime desc",
                    field = "a.id,a.itid,b.name,c.name[typ],nian,xf,isSucceed,a.createTime,a.inlearn",
                    pageSize = 50,
                    pageNo = 1,
                    where = "a.eid='" + Utility.employeeLogin.eid + "'"
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
        /// 立即学习保存
        /// </summary>
        /// <param name="itid"></param>
        /// <returns></returns>
        [HttpPost]
        public int saveLearn(int itid)
        {
            new BLL.edu_myitemBLL().save(Utility.employeeLogin.eid, itid);
            return 1;
        }
        /// <summary>
        /// 开始学习
        /// </summary>
        /// <param name="miid"></param>
        /// <returns></returns>
        [HttpPost]
        public int beginlearn(int miid)
        {
            new BLL.edu_myitemBLL().beginLearn(miid, Utility.employeeLogin.eid);
            return 1;
        }
        /// <summary>
        /// 取消学习
        /// </summary>
        /// <param name="miid"></param>
        /// <returns></returns>
        public int cancel(int miid) {
            new BLL.edu_myitemBLL().delete(miid);
            return 1;
        }
    }

    public class eduItemSearchFind
    {
        public int scid { get; set; }
        public string name { get; set; }
        public int typ { get; set; }
    }
}