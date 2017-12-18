using System;
using System.Text;
using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
    public class toolController : Controller
    {
        //
        // GET: /root/tool/
        //解析工具
        #region view
        public ActionResult index()
        {
            return View();
        }
        //替换规则
        public ActionResult replacegif()
        {
            return View();
        }
        //查看图片
        public ActionResult imgs()
        {
            return View();
        }
        #endregion

        #region from
        [HttpPost]//  /root/tool/save
        [ValidateInput(false)]
        public int save(Models.replacegif m)
        {
            return BLL.root.toolBLL.save(m);
        }
        [HttpPost]
        public int delete(int id)
        {
            BLL.root.toolBLL.delete(id);
            return 1;
        }
        [HttpGet]
        public void getdata()
        {
            try
            {
                Response.Clear();
                Models.paging pag = new Models.paging()
                {
                    table = "replacegif",
                    order = "id desc",
                    field = "*",
                    pageSize = Convert.ToInt32(Request.QueryString["limit"]),
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = "1=1"
                };
                Response.Write(BLL.pagingBLL.runLaypage(pag));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        //  /root/tool/getreplace
        [HttpGet]
        public void getreplace()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.root.toolBLL.getData());
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
