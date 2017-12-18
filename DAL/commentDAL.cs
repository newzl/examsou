/*
 * 创建人:   hllive
 * 创建时间: 2017/11/1 16:06:13
 * 描述:
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class commentDAL : IDisposable
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
        ~commentDAL() { Dispose(false); }
        #endregion

        public void save(Models.comment m)
        {
            string sql = "insert into comment(eid,[sid],content,evaluate)values(@eid,@sid,@content,@evaluate)";
            SqlParameter[] pars = { 
                new SqlParameter("@eid", m.eid),
                new SqlParameter("@sid", m.sid),
                new SqlParameter("@content", m.content),
                new SqlParameter("@evaluate", m.evaluate)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
    }
}
