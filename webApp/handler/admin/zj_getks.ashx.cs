using System;
using System.Web;
using System.Web.SessionState;

namespace webApp.handler.admin
{
    /// <summary>
    /// zj_getks 组卷获得科室，通过pid(部门)  /handler/admin/zj_getks.ashx
    /// </summary>
    public class zj_getks : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                int pid = Convert.ToInt32(context.Request["pid"]);
                string data = BLL.departmentBLL.get_zj_KS(Utility.adminLogin.companyID, pid);
                if (string.IsNullOrEmpty(data)) context.Response.Write("null");
                else context.Response.Write(data);
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