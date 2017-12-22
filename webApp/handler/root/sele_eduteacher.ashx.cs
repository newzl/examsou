using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApp.handler.root
{
    /// <summary>
    /// sele_eduteacher 的摘要说明
    /// </summary>
    public class sele_eduteacher : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";

                string data = BLL.root.edu_teacherBLL.GetName();
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