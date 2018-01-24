using System;
using System.Web;
using Newtonsoft.Json;

namespace webApp.handler
{
    /// <summary>
    /// ln_getlearnsx 顺序练习获得试题
    /// </summary>
    public class ln_getlearnsx : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                Models.learnsx m = new Models.learnsx
                {
                    miid = Convert.ToInt32(context.Request.QueryString["miid"]),
                    scid = Convert.ToInt32(context.Request.QueryString["scid"]),
                    stype = context.Request.QueryString["stype"],
                    fx = Convert.ToInt32(context.Request.QueryString["fx"]),
                    row = Convert.ToInt32(context.Request.QueryString["row"])
                };
                if (Utility.employeeLogin.isLogin)
                {
                    string data = BLL.learn.learnsxBLL.exeLearnsx(m);
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