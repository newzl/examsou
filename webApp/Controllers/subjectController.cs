using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Controllers
{
    [employeeAuthorize]
    public class subjectController : Controller
    {
        //
        // GET: /subject/
        #region view
        public ActionResult index()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            Models.subjectDetail m = BLL.subjectClassBLL.detailEntity(id, Utility.employeeLogin.eid);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult subjecttree(int id)
        {
            ViewBag.sid = id;
            return View();
        }
        public ActionResult learnsubject()
        {
            return View();
        }
        #endregion

        #region form
        /// <summary>
        /// 获得选择题库数据 /subject/selesubject
        /// </summary>
        public void selesubject()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.subjectClassBLL.seleSubject(employeeLogin.eid));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        /// <summary>
        /// 获得查看题库系列树形菜单  /subject/subjectnodes
        /// </summary>
        public void subjectnodes()
        {
            try
            {
                Response.Clear();
                Response.Write(BLL.subjectClassBLL.subjectTree(Convert.ToInt32(Request.QueryString["sid"])));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        /// <summary>
        /// 我的题库table
        /// </summary>/subject/tablelearnsubject
        public void tablelearnsubject()
        {
            try
            {
                Response.Clear();
                Models.paging pag = new Models.paging()
                {
                    table = "v_learnSubject",
                    order = "createTime desc",
                    field = "id,[sid],sname,[level],createTime,learns,counts,total,inlearn",
                    pageSize = 50,
                    pageNo = 1,
                    where = "eid='" + employeeLogin.eid + "'"
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
        /// 保存评论
        /// </summary>/subject/savecomment
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public int savecomment(Models.comment m)
        {
            m.eid = Utility.employeeLogin.eid;
            BLL.commentBLL.save(m);
            return 1;
        }
        /// <summary>
        /// 获得评论
        /// </summary>/subject/commentdata
        public void commentdata()
        {
            try
            {
                Response.Clear();
                string sb = "valid=1 and [sid]=" + Request.QueryString["sid"].ToString();
                Models.paging pag = new Models.paging()
                {
                    table = "v_comment",
                    order = "createTime desc",
                    field = "photo,ename,content,evaluate,createTime,reply",
                    pageSize = 10,
                    pageNo = Convert.ToInt32(Request.QueryString["page"]),
                    where = sb
                };
                Response.Write(BLL.pagingBLL.runpage(pag, "yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        /// <summary>
        /// 保存学习题库
        /// </summary>/subject/savelearnsubject
        /// <param name="sid"></param>
        /// <returns></returns>
        public int savelearnsubject(int sid)
        {
            return BLL.learnSubjectBLL.saveLearnSubject(Utility.employeeLogin.eid, sid);
        }
        //取消学习  /subject/cancel
        public int cancel(int lid)
        {
            BLL.learnSubjectBLL.cancel(lid, Utility.employeeLogin.eid);
            return 1;
        }
        //清空学习 
        public int empty(int lid)
        {
            BLL.learnSubjectBLL.empty(lid, Utility.employeeLogin.eid);
            return 1;
        }
        //开始学习
        public int beginlearn(int lid) {
            BLL.learnSubjectBLL.beginLearn(lid, Utility.employeeLogin.eid);
            return 1;
        }
        #endregion
    }
}
