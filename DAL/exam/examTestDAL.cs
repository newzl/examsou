using System;
using System.Data.SqlClient;
using System.Data;


namespace DAL.exam
{
    public class examTestDAL : IDisposable
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
        ~examTestDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 获得 题库试阅  用于首页点击题库给用户试阅100题中15道题
        /// </summary>
        /// <param name="sid">类型id</param>
        /// <returns></returns>
        public DataTable getExamTest(int sid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@sid",sid)
                                };
            return SqlHelper.RunProcedure("[ExamTest]", pars);
        }
    }
}
