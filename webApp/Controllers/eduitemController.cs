using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;
using Utility;

namespace webApp.Controllers
{
    [employeeAuthorize]
    public class eduitemController : Controller
    {
        #region view
        public ActionResult search()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(new BLL.root.edu_itemBLL().detail(id));
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
                sb.Append("valid=1 and isDel=0 and GETDATE() between startDate and endDate ");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    eduItemSearchFind m = JsonConvert.DeserializeObject<eduItemSearchFind>(wherejson);
                    if (m.scid >= 0) sb.Append(" and scid =" + m.scid);
                    if (m.name != "") sb.Append(" and (a.name like '%" + m.name.Trim() + "%' or bh like '%" + m.name.Trim() + "%')");
                    if (m.typ >= 0) sb.Append(" and typ=" + m.typ);
                }
                Models.paging pag = new Models.paging()
                {
                    table = "edu_item a left join vp_itemTyp b on a.typ=b.id left join (select id,itid from edu_myitem where eid='" + employeeLogin.eid + "') c on a.id=c.itid",
                    order = "a.createTime desc",
                    field = "a.id,pic,a.name,b.name[itype],xf,learns,bh,mustTime,startDate,endDate,c.id[miid]",
                    pageSize = 10,
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb.ToString()
                };
                Response.Write(BLL.pagingBLL.runpage(pag,null));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        [HttpPost]
        public int saveLearn(int itid)
        {
            new BLL.edu_myitemBLL().save(Utility.employeeLogin.eid, itid);
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