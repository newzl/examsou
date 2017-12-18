using System;
using System.Web;

namespace webApp.handler
{
    /// <summary>
    /// sele_jb_zc 的摘要说明
    /// </summary>
    public class sele_jb_zc : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                int pid = Convert.ToInt32(context.Request.QueryString["pid"]);
                string data = BLL.jbzcBLL.getSelect(0, pid);
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