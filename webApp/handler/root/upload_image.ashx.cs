using System;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace webApp.handler.root
{
    /// <summary>
    /// upload_image 上传图片，并返回图片url
    /// </summary>
    public class upload_image : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Charset = "UTF-8";
                // 初始化一大堆变量
                string inputname = "file";//表单文件域name
                string attachdir = "/Picture/photo";     // 上传文件保存路径，结尾不要带/
                string upext = "jpg,jpeg,gif,png";    // 上传扩展名jpg，png，gif，jpeg
                string oldpic = context.Request.QueryString["oldpic"];//原图片路径
                byte[] file;                     // 统一转换为byte数组处理
                string localname = "";
                string disposition = context.Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];

                if (disposition != null)// HTML5上传
                {
                    file = context.Request.BinaryRead(context.Request.TotalBytes);
                    localname = context.Server.UrlDecode(System.Text.RegularExpressions.Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value);// 读取原始文件名
                }
                else
                {
                    HttpFileCollection filecollection = context.Request.Files;
                    HttpPostedFile postedfile = filecollection.Get(inputname);
                    // 读取原始文件名
                    localname = postedfile.FileName;
                    // 初始化byte长度.
                    file = new Byte[postedfile.ContentLength];
                    // 转换为byte类型
                    System.IO.Stream stream = postedfile.InputStream;
                    stream.Read(file, 0, postedfile.ContentLength);
                    stream.Close();
                    filecollection = null;
                }
                if (file.Length == 0) throw new Exception(JsonConvert.SerializeObject(new { state = 0, msg = "无数据提交" }));
                else
                {
                    if (file.Length > 30720) throw new Exception(JsonConvert.SerializeObject(new { state = 0, msg = "文件大小超过30KB" }));
                    else
                    {
                        string attach_dir, attach_subdir, filename, extension, target;
                        extension = GetFileExt(localname);// 取上载文件后缀名
                        if (("," + upext + ",").IndexOf("," + extension + ",") < 0)
                        {
                            throw new Exception(JsonConvert.SerializeObject(new { state = 0, msg = "上传文件扩展名必需为：" + upext }));
                        }
                        else
                        {
                            //if (Utility.employeeLogin.isLogin)
                            //{
                                attach_subdir = DateTime.Now.ToString("yyMMdd");
                                attach_dir = attachdir + "/" + attach_subdir + "/";
                                // 生成随机文件名
                                Random random = new Random(DateTime.Now.Millisecond);
                                filename = DateTime.Now.ToString("HHmmssffff") + "." + extension;

                                target = attach_dir + filename;
                                try
                                {
                                    CreateFolder(context.Server.MapPath(attach_dir));
                                    //保存图片
                                    System.IO.FileStream fs = new System.IO.FileStream(context.Server.MapPath(target), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                    fs.Write(file, 0, file.Length);
                                    fs.Flush();
                                    fs.Close();
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(JsonConvert.SerializeObject(new { state = 0, msg = ex.Message }));
                                }
                                if (oldpic != "" && oldpic != null)
                                {
                                    Common.DirFile.DeleteFile(oldpic);//删除原来图片
                                }
                                //BLL.account.employeeBLL.updPhoto(target, Utility.employeeLogin.eid);
                                context.Response.Write(JsonConvert.SerializeObject(new { state = 1, msg = target }));
                            //}
                            //else
                            //{
                            //    throw new Exception(JsonConvert.SerializeObject(new { state = 4001, msg = "没有访问权限" }));
                            //}
                        }
                    }
                }
                file = null;
            }
            catch (Exception m)
            {
                context.Response.Write(m.Message);
            }
            finally { context.Response.End(); }
        }

        string GetFileExt(string FullPath)
        {
            if (FullPath != "") return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
            else return "";
        }

        void CreateFolder(string FolderPath)
        {
            if (!System.IO.Directory.Exists(FolderPath)) System.IO.Directory.CreateDirectory(FolderPath);
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