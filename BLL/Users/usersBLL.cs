using DAL.Users;
using Models;

namespace BLL.Users
{
    public class usersBLL
    {
        public static users login(string account, string pwd)
        {
            using (usersDAL dal = new usersDAL())
            {
                return dal.login(account, pwd);
            }
        }
        public static void lastLoginTime(int ID)
        {
            using (usersDAL dal = new usersDAL())
            {
                dal.lastLoginTime(ID);
            }
        }
        public static string execAdmHome(int ID)
        {
            using (usersDAL dal = new usersDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.execAdmHome(ID), null);
                }
            }
        }
    }
}
