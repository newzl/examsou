using System;
using System.Web;

namespace webApp.handler.root
{
    /// <summary>
    /// gettree 的摘要说明
    /// </summary>
    public class gettree : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                string qs = context.Request.QueryString["id"];
                string data = BLL.subjectClassBLL.getTree(string.IsNullOrEmpty(qs) ? 2 : Convert.ToInt32(qs));
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