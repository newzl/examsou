using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.exam
{
    public class formalDAL : IDisposable
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
        ~formalDAL() { Dispose(false); }
        #endregion

        /// <summary>
        /// 准备考试
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="code">返回状态码：1:成功，0:项目已失效</param>
        /// <returns></returns>
        public DataTable readyExam(string eid, ref int code)
        {
            SqlParameter[] pars = {
                new SqlParameter("@eid",eid),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[1].Direction = ParameterDirection.ReturnValue;
            DataTable dt = SqlHelper.RunProcedure("[exs_ready]", pars);
            code = Convert.ToInt32(pars[1].Value);
            return dt;
        }
        /// <summary>
        /// 获得正规考试试卷
        /// </summary>
        /// <param name="itid"></param>
        /// <param name="code">返回状态码：1:成功，0:项目已失效</param>
        /// <returns></returns>
        public DataSet getFormalPaper(int itid, ref int code)
        {
            SqlParameter[] pars = {
                new SqlParameter("@itid",itid),
                new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[1].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SqlHelper.RunProcedure("[exs_getFormalPaper]", pars, "formalPaper");
            code = Convert.ToInt32(pars[1].Value);
            return ds;
        }
        /// <summary>
        /// 保存正规考试试卷
        /// </summary>
        /// <param name="m"></param>
        public void save(Models.formalExam m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@miid",m.miid),
                new SqlParameter("@chlist",m.chlist),
                new SqlParameter("@mulist",m.mulist),
                new SqlParameter("@julist",m.julist),
                new SqlParameter("@chans",m.chans),
                new SqlParameter("@muans",m.muans),
                new SqlParameter("@juans",m.juans),
                new SqlParameter("@useTime",m.useTime)
            };
            SqlHelper.RunProcedures("[exs_marking]", pars);
        }
        /// <summary>
        /// 查看正规考试试卷
        /// </summary>
        /// <param name="ansid"></param>
        /// <returns></returns>
        public DataSet showFormal(int ansid) {
            SqlParameter[] pars = {
                new SqlParameter("@ansid",ansid)
            };
            return SqlHelper.RunProcedure("[exs_showFormal]", pars, "showFormal");
        }
    }
}