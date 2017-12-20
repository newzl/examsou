using Newtonsoft.Json;
using System;
using System.Web;
using Utility;

namespace webApp.handler
{
    /// <summary>
    /// sele_subjectClass 的摘要说明
    /// </summary>
    public class sele_subjectClass : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                if (employeeLogin.isLogin)
                {
                    int pid = Convert.ToInt32(context.Request.QueryString["pid"]);
                    string data = BLL.subjectClassBLL.sele(pid);
                    if (string.IsNullOrEmpty(data))
                    {
                        context.Response.Write("null");
                    }
                    else
                    {
                        context.Response.Write(data);
                    }
                }
                else throw new Exception(JsonConvert.SerializeObject(new { state = 4001, msg = "没有访问权限" }));
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