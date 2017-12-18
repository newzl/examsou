using Models;
using DAL.account;
using System.Data;
using System.Text;

namespace BLL.account
{
    public class employeeBLL
    {
        #region 账号登录
        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static employeeLoginInfo login(string account, string pwd)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.login(account, pwd);
            }
        }
        /// <summary>
        /// 通过eid获得前台登录数据
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static employeeCookie getCookieEntity(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getCookieEntity(eid);
            }
        }
        #endregion

        #region 注册
        /// <summary>
        /// 手机号码注册获得动态码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int sendPhoneCode(string phone)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                if (dal.phoneExists(phone))
                {
                    return -1;//手机号码已经注册
                }
                else
                {
                    if (validateEmailBLL.sendPhoneCode(phone))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 邮箱注册获得动态码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int sendEmailCode(string email)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                if (dal.emailExists(email))
                {
                    return -1;//邮箱已经注册
                }
                else
                {
                    if (validateEmailBLL.sendEmailCode(email))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pwd"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int regPhone(string phone, string pwd, string token)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.regPhone(phone, pwd, token);
            }
        }
        /// <summary>
        /// 邮箱注册
        /// </summary>
        /// <param name="account"></param>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int regEmail(string account, string email, string pwd, string token)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.regEmail(account, email, pwd, token);
            }
        }
        #endregion

        #region 找回密码
        /// <summary>
        /// 手机或邮箱获得动态码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int sendBackPwdCode(string account)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                if (dal.phoneOrEmailExists(account))
                {
                    if (isPhone(account))//如果是手机
                    {
                        if (validateEmailBLL.sendBackPwdPhoneCode(account)) return 1;
                        else return 0;
                    }
                    else//其他为邮箱
                    {
                        if (validateEmailBLL.sendBackPwdEmailCode(account)) return 1;
                        else return 0;
                    }
                }
                else
                {
                    return -1;//手机和邮箱不存在
                }
            }
        }
        public static bool isPhone(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^((\+)?86|((\+)?86)?)0?1[34578]\d{9}$");
        }
        //执行找回密码存储过程
        public static int backPwd(string account, string pwd, string token)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.backPwd(account, pwd, token);
            }
        }
        #endregion

        #region 后台管理
        /// <summary>
        /// 获得详细信息，用于后台查看用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static employeeDetail getDetail(int id)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getDetail(id);
            }
        }
        /// <summary>
        /// 修改认证状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="id"></param>
        public static void updateState(int state, int id)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                dal.updateState(state, id);
            }
        }
        #endregion

        #region 基本资料
        public static string profileCore(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{");
                    DataSet ds = dal.profileCore(eid);
                    sb.Append("\"info\":" + jp.ToJson(ds.Tables[0], null));
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        sb.Append(",\"table\":" + jp.ToJson(ds.Tables[1]));
                    }
                    else
                    {
                        sb.Append(",\"table\":null");
                    }
                    return sb.ToString() + "}";
                }
            }
        }
        //判断是否可以申请审核
        public static int isApply(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.isApply(eid);
            }
        }
        //修改密码
        public static int changePwd(string uniqueID, string oldPwd, string newPwd)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.changePwd(uniqueID, oldPwd, newPwd);
            }
        }
        /// <summary>
        /// 获得基本信息实体
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static employeePersonal getPersonalEntity(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getPersonalEntity(eid);
            }
        }
        /// <summary>
        /// 获得工作信息实体
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static employeeProfession getProfessionEntity(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getProfessionEntity(eid);
            }
        }
        /// <summary>
        /// 获得基本资料 部门科室，级别职称  实体
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static employee_bkjz getBkjzEntity(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getBkjzEntity(eid);
            }
        }
        /// <summary>
        /// 保存基本资料
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int saveEmployee(Models.employeePersonal m)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.saveEmployee(m);
            }
        }
        /// <summary>
        /// 保存工作信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int saveProfession(Models.employeeProfession m)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.saveProfession(m);
            }
        }
        /// <summary>
        /// 保存基本资料 部门科室，级别职称
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int saveBkjz(Models.employee_bkjz m)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.saveBkjz(m);
            }
        }
        /// <summary>
        /// 获得证件照的路径
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static string getPhoto(string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                return dal.getPhoto(eid);
            }
        }
        /// <summary>
        /// 上传证件照
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="eid"></param>
        public static void updPhoto(string photo, string eid)
        {
            using (employeeDAL dal = new employeeDAL())
            {
                dal.updPhoto(photo, eid);
            }
        }

        #endregion

    }
}