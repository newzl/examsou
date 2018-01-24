using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.exam
{
    /// <summary>
    /// 模拟考试
    /// </summary>
    public class simulateDAL : IDisposable
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
        ~simulateDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 通过项目ID获得模拟考试试卷
        /// </summary>
        /// <param name="itid"></param>
        /// <returns></returns>

        public DataSet getSimulatePaper(int itid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@itid",itid),
                 new SqlParameter("@retu",SqlDbType.Int)
            };
            pars[1].Direction = ParameterDirection.ReturnValue;
            DataSet ds = SqlHelper.RunProcedure("[exs_getSimulatePaper]", pars, "simulatePaper");
            if (Convert.ToInt32(pars[1].Value) > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查看模拟考试试卷
        /// </summary>
        /// <param name="ansid"></param>
        /// <returns></returns>
        public DataSet showSimulatePaper(int esid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@esid",esid)
            };
            return SqlHelper.RunProcedure("[exs_showSimulate]", pars, "showSimulate");
        }
        /// <summary>
        /// 保存模拟考试
        /// </summary>
        /// <param name="m"></param>
        public void save(Models.simulate m)
        {
            string sql = "insert into examSimulate(miid,chlist,mulist,julist,chanswer,muanswer,juanswer,score,usetime)values(@miid,@chlist,@mulist,@julist,@chanswer,@muanswer,@juanswer,@score,@usetime)";
            SqlParameter[] pars = {
                new SqlParameter("@miid",m.miid),
                new SqlParameter("@chlist",m.chlist),
                new SqlParameter("@mulist",m.mulist),
                new SqlParameter("@julist",m.julist),
                new SqlParameter("@chanswer",m.chans),
                new SqlParameter("@muanswer",m.muans),
                new SqlParameter("@juanswer",m.juans),
                new SqlParameter("@score",m.score),
                new SqlParameter("@usetime",m.usetime)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void delete(int id) {
            string sql = "delete examSimulate where id=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id",id)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
    }
}