using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Collections;


namespace webApp
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpServerUtility server = context.Server;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            HttpPostedFile file = context.Request.Files[0];
            if (file.ContentLength > 0) {
                string extName = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();
                string fullName = fileName + extName;
                string imageFilter = ".jpg|.png|.gif";
                if (imageFilter.Contains(extName.ToLower()))
                {
                    string phyFilePath = server.MapPath("~/UplodFiles/Image") + fileName;
                    file.SaveAs(phyFilePath);
                    response.Write("上传成功！文件名：" + fullName + "<br/>");
                     Hashtable hash=new Hashtable();
                    hash["code"]="0";
                    hash["msg"]="上传成功";
                    hash["data"]=fullName;
                    string kk="{'code':'0','msg':'上传成功','data'}";
                    response.Write("上传成功");
                }
            }



            //var postedFile = context.Request.Params["demo1"];
            //byte[] buffer = new byte[postedFile.Length];
            //string fileUrl, getUrl;
            //fileUrl = "/uploTeacher/" + DateTime.Now.ToString("yyMMdd") + "/";
            //// 生成随机文件名
            //Random random = new Random(DateTime.Now.Millisecond);
            //string fileName = DateTime.Now.ToString("hhmmss") + random.Next(10000) + "";
            //try
            //{
            //    var m = new MemoryStream(buffer);
            //    string ext = postedFile.Substring(postedFile.LastIndexOf("."));
            //    getUrl = fileUrl + fileName + ext;
            //    if (!Directory.Exists(HttpContext.Current.Server.MapPath(getUrl)))
            //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(getUrl));

            //    var path = HttpContext.Current.Server.MapPath(getUrl); ;
            //    var img = Image.FromStream(m);
            //    img.Save(path);
            //    m.Close();
            //    context.Response.Write(getUrl);
            //}
            //catch (Exception ex)
            //{
            //    context.Response.Write(ex.Message);
            //}
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