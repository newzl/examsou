﻿using System;
using System.Web;

namespace webApp.handler.admin
{
    /// <summary>
    /// sele_subjectClass 
    /// pid<0：通过leve获得
    /// level<0：通过pid获得
    /// </summary>
    public class sele_subjectClass : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                string req_level = context.Request.QueryString["level"];
                string req_pid = context.Request.QueryString["pid"];
                int level = -1, pid = -1;
                if (!string.IsNullOrEmpty(req_level))
                {
                    level = Convert.ToInt32(req_level);
                }
                else if (!string.IsNullOrEmpty(req_pid))
                {
                    pid = Convert.ToInt32(req_pid);
                }
                string data = BLL.subjectClassBLL.sele(level, pid);
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