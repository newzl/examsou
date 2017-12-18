using System.Web;

namespace Utility
{
    public class employeeLogin
    {
        public static bool isLogin
        {
            get { return HttpContext.Current.Session[globalValue.SESSION_EMPLOYEE_LOGININFO] != null ? true : false; }
        }
        //public static int id
        //{
        //    get { return (HttpContext.Current.Session[globalValue.SESSION_EMPLOYEE_LOGININFO] as Models.employeeSession).id; }
        //}
        public static string eid
        {
            get { return (HttpContext.Current.Session[globalValue.SESSION_EMPLOYEE_LOGININFO] as Models.employeeSession).eid; }
        }
        public static bool setLoginSession(Models.employeeSession entity)
        {
            HttpContext.Current.Session.Timeout = 480;//会话时间 单位分钟 8小时
            HttpContext.Current.Session[globalValue.SESSION_EMPLOYEE_LOGININFO] = entity;
            return isLogin;
        }
        public static void removeLogin()
        {
            HttpContext.Current.Session.Remove(globalValue.SESSION_EMPLOYEE_LOGININFO);
        }
    }
}
