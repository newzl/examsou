using System;
using System.Web;
using System.Web.SessionState;
using Utility;
using Newtonsoft.Json;

namespace webApp.handler
{
    /// <summary>
    /// sele_bmks 获得部门科室select数据
    /// </summary>
    public class sele_bmks : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                string companyID = context.Request.QueryString["companyid"];
                int pid = Convert.ToInt32(context.Request.QueryString["pid"]);
                int cid = 0;
                if (string.IsNullOrEmpty(companyID))
                {
                    if (adminLogin.isLogin)
                    {
                        cid = adminLogin.companyID;
                    }
                    else
                    {
                        throw new Exception(JsonConvert.SerializeObject(new { state = 4001, msg = "没有访问权限" }));
                    }
                }
                else
                {
                    cid = Convert.ToInt32(companyID);
                }
                string data = BLL.departmentBLL.getSelect(cid, pid);
                if (string.IsNullOrEmpty(data))
                {
                    context.Response.Write("null");
                }
                else
                {
                    context.Response.Write(data);
                }
            }
            catch (Exception m)
            {
                context.Response.Write(m.Message);
            }
            finally { context.Response.End(); }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}