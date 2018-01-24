using System;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace webApp.handler
{
    /// <summary>
    /// ln_getlearnks 的摘要说明
    /// </summary>
    public class ln_getlearnks : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                Models.learnks m = new Models.learnks
                {
                    scid = Convert.ToInt32(context.Request.QueryString["scid"]),
                    stype = context.Request.QueryString["stype"],
                    page = Convert.ToInt32(context.Request.QueryString["page"])
                };
                if (Utility.employeeLogin.isLogin)
                {
                    string data = BLL.learn.learnsxBLL.exeLearnks(m);
                    if (string.IsNullOrEmpty(data)) context.Response.Write("null");
                    else context.Response.Write(data);
                }
                else
                {
                    throw new Exception(JsonConvert.SerializeObject(new { state = 4001, msg = "没有访问权限" }));
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