using Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Utility;

namespace webApp.Areas.admin.Controllers
{
    [adminAuthorize]
    public class papermsgController : Controller
    {
        //
        // GET: /admin/papermsg/

        public ActionResult planlist()
        {
            return View();
        }
        [HttpGet]
        public void table_planlist()
        {
            try
            {
                Response.Clear();
                string whereJson = Request.QueryString["whereJson"];
                string where = " companyID=" + adminLogin.companyID;
                if (!string.IsNullOrEmpty(whereJson))
                {
                    planListWhere w = JsonConvert.DeserializeObject<planListWhere>(whereJson);
                    if (w.ename != "") where += " and name like '%" + w.ename + "%'";
                    if (w.type != -1) where += " and type=" + w.type.ToString();
                    if (w.islimit != -1) where += " and isLimit=" + w.islimit.ToString();
                    if (w.minNums != "") where += " and nums>=" + w.minNums;
                    if (w.maxNums != "") where += " and nums<=" + w.maxNums;
                    if (w.minScores != "") where += " and scores>=" + w.minScores;
                    if (w.maxScores != "") where += " and scores<=" + w.maxScores;
                    if (w.minPass != "") where += " and passScore>=" + w.minPass;
                    if (w.maxPass != "") where += " and passScore<=" + w.maxPass;
                    if (w.minDate != "") where += " and createTime>='" + w.minDate + "'";
                    if (w.maxDate != "") where += " and createTime<='" + w.maxDate + " 23:59:59'";
                    if (w.state != -1) where += " and state=" + w.state.ToString();
                }
                paging entity = new paging();
                entity.table = "v_examPlanList";
                entity.field = "id,name,type,examtype,nums,scores,passScore,examNum,useTime,isLimit,state,createTime";
                entity.order = "createTime desc";
                entity.where = where;
                entity.pageSize = Convert.ToInt32(Request.QueryString["limit"]);
                entity.pageNo = Convert.ToInt32(Request.QueryString["page"]);

                Response.Write(BLL.pagingBLL.runLaypage(entity, null));
            }
            catch (Exception m)
            {
                Response.Write(m.Message);
            }
            finally { Response.End(); }
        }
        [HttpDelete]// Delete: /admin/papermsg/delete
        public int delete(int epid)
        {
            BLL.examPlanBLL.delete_plan(epid);
            return 1;
        }
    }
    public class planListWhere
    {
        public string ename { get; set; }
        public int type { get; set; }
        public int islimit { get; set; }
        public string minNums { get; set; }
        public string maxNums { get; set; }
        public string minScores { get; set; }
        public string maxScores { get; set; }
        public string minPass { get; set; }
        public string maxPass { get; set; }
        public string minDate { get; set; }
        public string maxDate { get; set; }
        public int state { get; set; }
    }
}
