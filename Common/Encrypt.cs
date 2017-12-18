using System;
using System.Text;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// 字符串加密
    /// </summary>
    public class Encrypt
    {
        //MD5加密算法
        public static string md5(string str)
        {
            string pwd = String.Empty;
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                pwd += s[i].ToString("X2");
            }
            return pwd;
        }
    }
}
