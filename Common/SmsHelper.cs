using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Common
{
    public class SmsHelper
    {
        //短信平台用户名和密码
        private static string user_name = "13765176285", user_pwd = "EFED17DF45BF54756B3F170F99B4";

        /// <summary>
        /// 向移动设备发送信息，并存入数据库
        /// </summary>
        /// <param name="mobiles">接收信息手机号，多个号码用逗号“,”格开</param>
        /// <param name="content">发送信息内容</param>
        /// <returns></returns>
        public static string Sms_Send(string mobiles, string content)
        {
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", user_name);
            sms.AppendFormat("&pwd={0}", user_pwd);//登陆平台，管理中心--基本资料--接口密码（28位密文）；复制使用即可。
            sms.AppendFormat("&content={0}", content);
            sms.AppendFormat("&mobile={0}", mobiles);
            sms.AppendFormat("&sign={0}", "新智联在线考试");// 公司的简称或产品的简称都可以
            sms.Append("&type=pt");
            string sms_service = ConfigurationManager.AppSettings["sms_send_service"].ToString();//短信平台web服务地址
            string resp = PushToWeb(sms_service, sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                return msg[1];//返回SendID
                //string sql = "insert into Msg_Send(s_Mobile,s_Content,s_SendID) values('" + mobiles + "','" + content + "','" + msg[1] + "')";
                //try
                //{
                //    DBHelper.DataExecute(sql);
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            }
            else
            {
                return "0";
            }
            //return msg[1];//返回SendID
        }
        /// <summary>
        /// 主动获取上行（平台收到的所有信息）并存入数据库
        /// </summary>
        /// <param name="mobiles">发送信息的手机号，多个号码用逗号“,”格开</param>
        /// <param name="content">发送信息内容</param>
        /// <returns></returns>
        //public static string Sms_OneselfGetMo()
        //{
        //    StringBuilder sms = new StringBuilder();
        //    sms.AppendFormat("name={0}", user_name);
        //    sms.AppendFormat("&pwd={0}", user_pwd);//登陆平台，管理中心--基本资料--接口密码（28位密文）；复制使用即可。
        //    sms.AppendFormat("&type=mo");
        //    string sms_service = ConfigurationManager.AppSettings["sms_mo_service"].ToString();//自主获取短信回得web服务地址
        //    string args = PushToWeb(sms_service, sms.ToString(), Encoding.UTF8);
        //    if (args != null)
        //    {
        //        SaveMo("OneselfGet", null, args);
        //    }
        //    return args;
        //}
        /// <summary>
        /// 短信平台自动推送上行（当前回复的信息）并存入数据库
        /// </summary>
        /// <returns></returns>
        //public static bool Sms_PlatformPushMo(string name, string pwd, string args)
        //{
        //    return SaveMo(name, pwd, args);
        //}
        #region 将移动设备回复信息存入数据库
        /// <summary>
        /// 将移动设备回复信息存入数据库
        /// </summary>
        /// <param name="name">交到用户名</param>
        /// <param name="pwd">交互密码</param>
        /// <param name="args">上行数据信息</param>
        /// <returns></returns>
        //private static bool SaveMo(string name, string pwd, string args)
        //{
        //    bool flag = false;
        //    if (args != null && args.Contains("#@@#"))//多条上行一起推送过来的
        //    {
        //        string[] allmo = args.Split(new string[] { "#@@#" }, StringSplitOptions.RemoveEmptyEntries);//拆分成一条一条的信息，放到数组中
        //        for (int i = 0; i < allmo.Length; i++)
        //        {
        //            string[] mo = allmo[i].Split(new string[] { "#@#" }, StringSplitOptions.None);//这个地方要用None，空值不能移除
        //            //mo[0]  回复的手机号码
        //            //mo[1]  回复的内容
        //            //mo[2]  回复的时间
        //            //mo[3]  系统扩展码+发送时带的extno值  一般情况下账号的特服号即为系统扩展码。 如账号的特服号是 1001，发送时带的extno=888， mo[3]=1001888

        //            //将 上面信息插入数据库即可
        //            string sql = "insert into msg_mo(m_name,m_pwd,m_mobile,m_content,m_time,m_extno) values('" + name + "','" + pwd + "','" + mo[0] + "','" + mo[1] + "','" + Convert.ToDateTime(mo[2]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," + mo[3] + ")";
        //            try
        //            {
        //                DBHelper.DataExecute(sql);
        //                flag = true;
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    else if (args != null)//只有一条上行信息
        //    {
        //        string[] mo = args.Split(new string[] { "#@#" }, StringSplitOptions.None);//这个地方要用None，空值不能移除
        //        //mo[0]  回复的手机号码
        //        //mo[1]  回复的内容
        //        //mo[2]  回复的时间
        //        //mo[3]  系统扩展码+发送时带的extno值  一般情况下账号的特服号即为系统扩展码。 如账号的特服号是 1001，发送时带的extno=888， mo[3]=1001888

        //        //将 上面信息插入数据库即可
        //        string sql = "insert into msg_mo(m_name,m_pwd,m_mobile,m_content,m_time,m_extno) values('" + name + "','" + pwd + "','" + mo[0] + "','" + mo[1] + "','" + Convert.ToDateTime(mo[2]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'," + mo[3] + ")";
        //        try
        //        {
        //            DBHelper.DataExecute(sql);
        //            flag = true;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    return flag;
        //} 
        #endregion
        /// <summary>
        /// 获取短信返回信息
        /// </summary>
        /// <param name="weburl">短信平台web服务地址</param>
        /// <param name="data">传往web服务地址的参数</param>
        /// <param name="encode">字符编码形式</param>
        /// <returns></returns>
        private static string PushToWeb(string weburl, string data, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            //接收返回信息：
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader aspx = new StreamReader(response.GetResponseStream(), encode);
            return aspx.ReadToEnd();
        }
    }
}
