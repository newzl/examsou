using System;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class edu_recordDAL : IDisposable
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
        ~edu_recordDAL() { Dispose(false); }
        #endregion

        /// <summary>
        /// 获得正在学习课件list
        /// </summary>/course/learn/
        /// <param name="miid"></param>
        /// <returns></returns>
        public DataTable getCourse(int miid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@miid",miid)
            };
            return SqlHelper.RunProcedure("edu_course", pars);
        }
        /// <summary>
        /// 保存学习记录
        /// </summary>
        /// <param name="m"></param>
        public void saveRecord(Models.edu_record m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@miid",m.miid),
                new SqlParameter("@kjid",m.kjid),
                new SqlParameter("@minut",m.minut),
                new SqlParameter("@timer",m.timer),
                new SqlParameter("@position",m.position)
            };
            SqlHelper.RunProcedure("edu_saveRecord", pars);
        }
        
    }
}