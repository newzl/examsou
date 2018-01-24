using Newtonsoft.Json;
using System;
using System.Web;

namespace webApp.handler
{
    /// <summary>
    /// edu_saverecord 保存学习记录
    /// /handler/edu_saverecord.ashx
    /// </summary>
    public class edu_saverecord : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                Models.edu_record m = new Models.edu_record
                {
                    miid = Convert.ToInt32(context.Request.Form["miid"]),
                    kjid = Convert.ToInt32(context.Request.Form["kjid"]),
                    minut = Convert.ToInt32(context.Request.Form["minut"]),
                    timer = context.Request.Form["timer"],
                    position = Convert.ToInt32(context.Request.Form["position"])
                };
                if (Utility.employeeLogin.isLogin)
                {
                    new BLL.edu_recordBLL().saveRecord(m);
                    context.Response.Write(JsonConvert.SerializeObject(new { state = 200 }));
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