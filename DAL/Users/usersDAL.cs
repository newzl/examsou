using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL.Users
{
    public class usersDAL : IDisposable
    {
        #region 释放资源
        bool dis;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (dis) return;
            if (disposing)
                dis = true;
        }
        ~usersDAL() { Dispose(false); }
        #endregion

        public users login(string account, string pwd)
        {
            string sql = "select ID,companyID,type from Users where (logID=@account or mobilePhone=@account) and [PassWord]=@pwd";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@account",account),
                new SqlParameter("@pwd",pwd)
            };
            DataTable dt = SqlHelper.ExcuteDataTable(sql, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                users entity = new users();
                entity.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                entity.companyID = dt.Rows[0]["companyID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["companyID"]) : 0;
                entity.type = Convert.ToInt32(dt.Rows[0]["type"]);
                return entity;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="ID"></param>
        public void lastLoginTime(int ID)
        {
            string sql = "update Users set lastLoginTime=GETDATE() where ID=@ID";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ID",ID)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        /// <summary>
        /// 获得存储过程中的数据
        /// </summary>20171012
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable execAdmHome(int ID)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ID",ID)
            };
            return SqlHelper.RunProcedure("[adm_home]", param);
        }
    }
}
