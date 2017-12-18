using System;
using System.Web.SessionState;
using System.Web;
using Newtonsoft.Json;

namespace webApp.handler
{
    /// <summary>
    /// ln_saveLearn 保存学习
    /// </summary>
    public class ln_saveLearn : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                Models.saveLearn m = new Models.saveLearn
                {
                    lid = Convert.ToInt32(context.Request.Form["lid"]),
                    sid = Convert.ToInt32(context.Request.Form["sid"]),
                    stype = context.Request.Form["stype"],
                    row = Convert.ToInt32(context.Request.Form["row"]),
                    kid = Convert.ToInt32(context.Request.Form["kid"]),
                    answer = context.Request.Form["answer"],
                    result = Convert.ToBoolean(context.Request.Form["result"]),
                };
                if (Utility.employeeLogin.isLogin)
                {
                    context.Response.Write(BLL.learn.learnsxBLL.exeSaveLearn(m));
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