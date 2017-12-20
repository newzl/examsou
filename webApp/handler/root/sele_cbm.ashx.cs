using Newtonsoft.Json;
using System;
using System.Web;
using Utility;

namespace webApp.handler.root
{
    /// <summary>
    /// sele_cbm 的摘要说明
    /// </summary>
    public class sele_cbm : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                int type = Convert.ToInt32(context.Request.QueryString["type"]);
                string data = BLL.publicBLL.getcbm(type);
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