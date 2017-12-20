using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

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
        /// <summary>  
        /// C# DES加密方法  
        /// </summary>  
        /// <param name="value">要加密的字符串</param>  
        /// <param name="skey">密钥</param>  
        /// <returns>加密后的字符串</returns>  
        public static string DESEncrypt(string value, string skey)
        {
            using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(skey), IV = Encoding.UTF8.GetBytes(skey) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(value);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        /// <summary>  
        /// C# DES解密方法  
        /// </summary>  
        /// <param name="value">待解密的字符串</param>  
        /// <param name="skey">密钥</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DESDecrypt(string value, string skey)
        {
            using (DESCryptoServiceProvider sa = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(skey), IV = Encoding.UTF8.GetBytes(skey) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(value);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}