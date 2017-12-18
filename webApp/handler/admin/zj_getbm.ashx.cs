using System;
using System.Web;
using System.Web.SessionState;

namespace webApp.handler.admin
{
    /// <summary>
    /// zj_get_bm 组卷获得部门  /handler/admin/zj_getbm.ashx
    /// </summary>
    public class zj_getbm : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                string data = BLL.departmentBLL.get_zj_BM(Utility.adminLogin.companyID);
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