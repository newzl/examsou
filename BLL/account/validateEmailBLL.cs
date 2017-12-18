/*
 * 创建人:   hllive
 * 创建时间: 2017/10/18 10:01:31
 * 描述:
 */

using System;
namespace BLL.account
{
    public class validateEmailBLL
    {
        public static bool saveValidateEmail(Models.validateEmail entity)
        {
            DAL.account.validateEmailDAL dal = new DAL.account.validateEmailDAL();
            return dal.saveValidateEmail(entity);
        }
        /// <summary>
        /// 发送手机注册动态码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool sendPhoneCode(string phone)
        {
            string token = Token(6);
            string content = "尊敬的用户您好，您正在注册新智联在线考试账户，验证码为：" + token + "（确认是您本人操作，请勿将验证码告知他人）";
            string sendID = Common.SmsHelper.Sms_Send(phone, content);
            if (sendID != "0")//发送手机验证码成功
            {
                Models.validateEmail entity = new Models.validateEmail()
                {
                    account = phone,
                    token = token,
                    sendID = sendID,
                    vtype = 0
                };
                if (saveValidateEmail(entity))//存入数据库
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发送手机找回密码动态码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool sendBackPwdPhoneCode(string phone)
        {
            string token = Token(6);
            string content = "尊敬的用户您好，您正在找回密码，验证码为：" + token + "（确认是您本人操作，请勿将验证码告知他人）";
            string sendID = Common.SmsHelper.Sms_Send(phone, content);
            if (sendID != "0")//发送手机验证码成功
            {
                Models.validateEmail entity = new Models.validateEmail()
                {
                    account = phone,
                    token = token,
                    sendID = sendID,
                    vtype = 1
                };
                if (saveValidateEmail(entity))//存入数据库
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发送邮箱注册动态码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool sendEmailCode(string email)
        {
            string token = Token(6);
            string content = "<div style=\"margin:0 auto;width:500px;border:1px solid #ccc;border-radius: 15px;padding:20px;background-color:#fdfdfd\">";
            content += "<div style=\"font-size:17px;color:#7b7b7b;padding-top:20px;font-weight:bold;line-height:40px;\">尊敬的&nbsp;" + email + "：<div style=\"text-indent:2em;\">您好！</div></div>";
            content += "<p style=\"text-indent:2em;color: #555;\">您正在注册新智联在线考试账户,以下是您的邮箱验证码</p>";
            content += "<p style=\"color: #555;\">此验证码在30分钟内有效。</p>";
            content += "<p style=\"font-size:22px;color:#f50;font-weight:bold;\">" + token + "</p>";
            content += "<p style=\"color:#999;font-size:12px\">若您并未注册账户，请忽略本邮件，由此给您带来的不便请谅解。</p>";
            content += "<p style=\"color:#999;font-size:12px\">本邮件由系统自动发出，请勿直接回复！</p>";
            content += "<p style=\"text-align:right;\"><a href=\"http://www.zxxx.co/\" style=\"text-decoration:none;color:#333\">新智联在线考试</a></p></div>";
            if (Common.EmailHelper.SendEmail(email, "新智联在线考试会员注册", content))
            {
                Models.validateEmail entity = new Models.validateEmail()
                {
                    account = email,
                    token = token,
                    sendID = null,
                    vtype = 0
                };
                if (saveValidateEmail(entity))//存入数据库
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发送邮箱找回密码动态码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool sendBackPwdEmailCode(string email)
        {
            string token = Token(6);
            string content = "<div style=\"margin:0 auto;width:500px;border:1px solid #ccc;border-radius: 15px;padding:20px;background-color:#fdfdfd\">";
            content += "<div style=\"font-size:17px;color:#7b7b7b;padding-top:20px;font-weight:bold;line-height:40px;\">尊敬的&nbsp;" + email + "：<div style=\"text-indent:2em;\">您好！</div></div>";
            content += "<p style=\"text-indent:2em;color: #555;\">您正在重置账户密码,以下是您的邮箱验证码</p>";
            content += "<p style=\"color: #555;\">此验证码在30分钟内有效。</p>";
            content += "<p style=\"font-size:22px;color:#f50;font-weight:bold;\">" + token + "</p>";
            content += "<p style=\"color:#999;font-size:12px\">若您并未重置密码，请忽略本邮件，由此给您带来的不便请谅解。</p>";
            content += "<p style=\"color:#999;font-size:12px\">本邮件由系统自动发出，请勿直接回复！</p>";
            content += "<p style=\"text-align:right;\"><a href=\"http://www.zxxx.co/\" style=\"text-decoration:none;color:#333\">新智联在线考试</a></p></div>";
            if (Common.EmailHelper.SendEmail(email, "新智联在线考试重置密码", content))
            {
                Models.validateEmail entity = new Models.validateEmail()
                {
                    account = email,
                    token = token,
                    sendID = null,
                    vtype = 1
                };
                if (saveValidateEmail(entity))//存入数据库
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        public static string Token(int Length)
        {
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
