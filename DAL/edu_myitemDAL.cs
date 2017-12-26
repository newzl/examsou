using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class edu_myitemDAL : IDisposable
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
        ~edu_myitemDAL() { Dispose(false); }
        #endregion

        public void save(string eid, int itid)
        {
            string sql = "begin tran update edu_myitem set inlearn=0 where inlearn=1 and eid=@eid";
            sql += " insert into edu_myitem(eid,itid,inlearn)values(@eid,@itid,1)";
            sql += " update edu_item set learns+=1 where id=@itid commit tran";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@eid",eid),
                new SqlParameter("@itid",itid)
            };
            SqlHelper.ExcuteNonQuery(sql, para);
        }
        public void delete(int id)
        {
            string sql = "delete edu_myitem where id=@id";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@id",id)
            };
            SqlHelper.ExcuteNonQuery(sql, para);
        }
        public void beginLearn(int miid, string eid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@miid",miid),
                                new SqlParameter("@eid",eid)
                                };
            SqlHelper.RunProcedure("edu_beginLearn", pars);
        }
        //我的学习页获得数据
        public DataSet edu_index(string eid) {
            SqlParameter[] pars = {
                new SqlParameter("@eid",eid)
            };
           return SqlHelper.RunProcedure("edu_index", pars,"eduIndes");
        }
    }
}
