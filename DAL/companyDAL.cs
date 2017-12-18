/*
 * 创建人:   hllive
 * 创建时间: 2017/10/25 14:44:48
 * 描述:单位数据库操作类
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class companyDAL : IDisposable
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
            if (disposing) dis = true;
        }
        ~companyDAL() { Dispose(false); }
        #endregion

        public DataTable getcompany(string xzbm)
        {
            string sql = "select ID[val],name[text] from company where CityID=@CityID";
            SqlParameter[] pars = { 
                new SqlParameter("@CityID", xzbm)
            };
            return SqlHelper.ExcuteDataTable(sql, pars);
        }
    }
}
