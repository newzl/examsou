﻿using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Mvc;

namespace webApp.Areas.root.Controllers
{
     [Utility.adminAuthorize]
    public class manMuController : Controller
    {
        //
        // GET: /root/manMu/

        #region view
        public ActionResult list()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View(BLL.root.submuBLL.getEntity(id));
        }
        #endregion

        #region form
        [HttpGet]
        public void listdata()
        {
            try
            {
                Response.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("[State]=1");
                string wherejson = Request.QueryString["wherejson"];
                if (!string.IsNullOrEmpty(wherejson))
                {
                    Models.subFind m = JsonConvert.DeserializeObject<Models.subFind>(wherejson);
                    if (m.sid != 0) sb.Append(" and SubjectClassID in (select id from f_getChildren(" + m.sid + "))");
                    if (m.title != "") sb.Append(" and Title like '%" + m.title.Trim() + "%'");
                    if (m.inputState != -1) sb.Append(" and inputState=" + m.inputState);
                    if (m.mindate != "") sb.Append(" and k.createTime>='" + m.mindate.Trim() + "'");
                    if (m.maxdate != "") sb.Append(" and k.createTime<='" + m.maxdate.Trim() + " 23:59:59'");
                }
                Models.paging pag = new Models.paging()
                {
                    table = "MultinomialSubject k join subjectClass s on k.SubjectClassID=s.ID",
                    order = "k.createTime desc",
                    field = "k.id,s.name[sname],title,result,A,B,C,D,E,F,k.createTime[date],inputState[state],k.total,yes",
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

        [HttpPost]// /root/manmu/save
        [ValidateInput(false)]
        public int save(Models.submu m)
        {
            m.creator = Utility.adminLogin.ID;
            return BLL.root.submuBLL.save(m);
        }
        //修改入库状态
        [HttpPost]
        public int instate(int id, bool state)
        {
            BLL.root.submuBLL.inState(id, state);
            return 1;
        }
        [HttpGet]
        public JsonResult getentity(int id)
        {
            return Json(BLL.root.submuBLL.getEntity(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
