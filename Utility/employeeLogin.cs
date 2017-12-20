using System;
using System.Web;

namespace Utility
{
    public class employeeLogin
    {
        public static bool isLogin
        {
            get { return HttpContext.Current.Request.Cookies[globalValue.COOKIE_EMPLOYEE] != null ? true : false; }
        }
        public static string eid
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[globalValue.COOKIE_EMPLOYEE];
                if (cookie != null) return Common.Encrypt.DESDecrypt(cookie.Value, globalValue.DES_KEY);
                else return null;
            }
        }
        public static bool setLogin(string eid, bool auto)
        {
            HttpCookie cookie = new HttpCookie(
                globalValue.COOKIE_EMPLOYEE,
                Common.Encrypt.DESEncrypt(eid, globalValue.DES_KEY)
            );
            if (auto) cookie.Expires = DateTime.Now.AddMonths(2);//两个月过期
            HttpContext.Current.Response.Cookies.Add(cookie);
            return isLogin;
        }
        public static void removeLogin()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[globalValue.COOKIE_EMPLOYEE];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
