using System.Web;

namespace Utility
{
    public class adminLogin
    {
        public static bool isLogin
        {
            get { return HttpContext.Current.Session[globalValue.SESSION_ADMIN_LOGININFO] != null ? true : false; }
        }
        public static int ID
        {
            get { return (HttpContext.Current.Session[globalValue.SESSION_ADMIN_LOGININFO] as Models.users).ID; }
        }
        public static int companyID
        {
            get { return (HttpContext.Current.Session[globalValue.SESSION_ADMIN_LOGININFO] as Models.users).companyID; }
        }
        public static bool setLoginSession(Models.users entity)
        {
            HttpContext.Current.Session.Timeout = 360;//会话时间 单位分钟 6小时
            HttpContext.Current.Session[globalValue.SESSION_ADMIN_LOGININFO] = entity;
            return isLogin;
        }
        public static void removeLogin()
        {
            HttpContext.Current.Session.Remove(globalValue.SESSION_ADMIN_LOGININFO);
        }
    }
}
