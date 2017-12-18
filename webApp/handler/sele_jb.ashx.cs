using System;
using System.Web;
namespace webApp.handler
{
    /// <summary>
    /// sele_jb 获得级别（包括公共题库）
    /// </summary>
    public class sele_jb : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.Write(BLL.jbzcBLL.getAllJb());
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