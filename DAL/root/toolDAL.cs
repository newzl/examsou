/*
 * 创建人:   hllive
 * 创建时间: 2017/11/24 11:23:50
 * 描述:截图工具--替换规则
 */
using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.root
{
    public class toolDAL : IDisposable
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
        ~toolDAL() { Dispose(false); }
        #endregion
        public int save(replacegif m)
        {
            string sql = "if not exists(select * from replacegif where gif=@gif)begin insert into replacegif(rgex,gif,n) values(@rgex,@gif,@n) end";
            SqlParameter[] pars = { 
                new SqlParameter("@rgex", m.rgex),
                new SqlParameter("@gif ", m.gif),
                new SqlParameter("@n ", m.n)
            };
            return SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void delete(int id)
        {
            string sql = "delete replacegif where id=@id";
            SqlParameter[] pars = { 
                new SqlParameter("@id ", id)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }

        public DataTable getData()
        {
            return SqlHelper.ExcuteDataTable("select rgex[r],n from replacegif", null);
        }
    }
}
