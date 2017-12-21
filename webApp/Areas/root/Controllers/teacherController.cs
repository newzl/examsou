using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Drawing;
using System.IO;
using Models;
using BLL;


namespace webApp.Areas.root.Controllers
{
    public class teacherController : Controller
    {
        //
        // GET: /root/teacher/
        public ActionResult teacherList()
        {
            return View();
        }
        //[HttpGet]
        //public JsonResult uploadurl(string uploa)
        //{
        //    string fileUrl, getUrl;
        //    fileUrl = "/uploTeacher/" + DateTime.Now.ToString("yyMMdd") + "/";
        //    // 生成随机文件名
        //    Random random = new Random(DateTime.Now.Millisecond);
        //    string fileName = DateTime.Now.ToString("hhmmss") + random.Next(10000) + "";
        //    if (uploa != null && uploa.Length > 0)
        //    {
        //        getUrl = fileUrl + fileName+uploa.Substring(uploa.LastIndexOf("."));
        //        System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(fileUrl));
        //      uploa.se
        //    }
        //}

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
                    Models.subFind m = JsonConvert.DeserializeObject<Models.subFind>(wherejson);
                    if (m.leid != "") sb.Append(" and zc='" + m.leid + "'");
                    //if (m.zid != "") sb.Append(" and sid=" + m.zid);
                    if (m.title != "") sb.Append(" and name like '%" + m.title.Trim() + "%'");
                    //if (m.inputState != -1) sb.Append(" and typ=" + m.inputState);
                    //if (m.mindate != "") sb.Append(" and k.createTime>='" + m.mindate.Trim() + "'");
                    //if (m.maxdate != "") sb.Append(" and k.createTime<='" + m.maxdate.Trim() + " 23:59:59'");
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

            //objkeJian.cont = DirFile.WriteInFileStream(objkeJian.cont, objkeJian.curl);
            int result = new BLL.root.edu_teacherBLL().save(objedu_item);
            //if (result != 0 && objkeJian.curl != "0")
            //    DirFile.DeleteFile(objkeJian.curl);
            return result;

        }
        [HttpGet]
        public JsonResult getList(int id)
        {
            edu_teacher objList = new BLL.root.edu_teacherBLL().getTeacher(id);
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Del(int id)
        {
            int result = new BLL.root.edu_teacherBLL().DelTeacher(id);
            return result;
        }

        [HttpPost]
        public string GetImg(string img)
        {
            //HttpRequestBase filecollection =HttpContext.Request.Files;
            //HttpPostedFile postedfile = filecollection.Get(inputname);
            //HttpFileCollection filecollection = HttpContext context.Request.Files;
            //HttpFileCollection file=Request.Files;
            //HttpPostedFile postedfile = filecollection.Get(inputname);
            //HttpPostedFile file = Request.Files[0];
            string c = Request.Params["test1"];
            return uploadImage(img);
        }

        public string uploadImage(string fileImg)
        {
            var postedFile = fileImg;
            byte[] buffer = new byte[postedFile.Length];
            string fileUrl, getUrl;
            fileUrl = "/images/teacher/" + DateTime.Now.ToString("yyMMdd") + "/";
            // 生成随机文件名
            Random random = new Random(DateTime.Now.Millisecond);
            string fileName = DateTime.Now.ToString("hhmmss") + random.Next(10000) + "";
            try
            {
                var m = new MemoryStream(buffer);
                string ext = postedFile.Substring(postedFile.LastIndexOf("."));
                getUrl = fileUrl + fileName + ext;
                if (!Directory.Exists(Server.MapPath(getUrl)))
                    Directory.CreateDirectory(Server.MapPath(getUrl));
                System.IO.FileStream fs = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath(getUrl), System.IO.FileMode.Create);
                var path = Server.MapPath(getUrl); ;
                var img = Image.FromStream(m);
                img.Save(path);
                m.Close();
                return getUrl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}