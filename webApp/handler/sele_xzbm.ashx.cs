using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.SessionState;
using Utility;
namespace webApp.handler
{
    /// <summary>
    /// sele_xzbm 的摘要说明
    /// </summary>
    public class sele_xzbm : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                if (employeeLogin.isLogin)
                {
                    string pxzbm = context.Request.QueryString["pxzbm"];
                    string data = BLL.publicBLL.getxzbm(pxzbm);
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