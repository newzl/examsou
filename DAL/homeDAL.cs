using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class homeDAL : IDisposable
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
        ~homeDAL() { Dispose(false); }
        #endregion

        public DataSet execHome(string eid)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.RunProcedure("[home]", param, "home");
        }
    }
}
