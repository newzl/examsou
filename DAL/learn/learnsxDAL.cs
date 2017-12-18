/*
 * 创建人:   hllive
 * 创建时间: 2017/11/10 14:36:27
 * 描述:顺序学习数据库操作
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.learn
{
    public class learnsxDAL : IDisposable
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
        ~learnsxDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 顺序练习执行[ln_learnsx]存储过程
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DataTable exeLearnsx(Models.learnsx m)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@lid",m.lid),
                                new SqlParameter("@sid",m.sid),
                                new SqlParameter("@stype",m.stype),
                                new SqlParameter("@fx",m.fx),
                                new SqlParameter("@row",m.row)
                                };
            return SqlHelper.RunProcedure("[ln_learnsx]", pars);
        }
        /// <summary>
        /// 获得快速背题
        /// </summary>
        /// <param name="m"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataTable exeLearnks(Models.learnks m, out int count)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@sid",m.sid),
                                new SqlParameter("@stype",m.stype),
                                new SqlParameter("@pageNo",m.page),
                                new SqlParameter("@groups",SqlDbType.Int)
                                };
            pars[3].Direction = ParameterDirection.Output;
            DataTable dt = SqlHelper.RunProcedure("[ln_learnks]", pars);
            count = Convert.ToInt32(pars[3].Value);
            return dt;
        }
        /// <summary>
        /// 保存学习
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int exeSaveLearn(Models.saveLearn m)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@lid",m.lid),
                                new SqlParameter("@sid",m.sid),
                                new SqlParameter("@stype",m.stype),
                                new SqlParameter("@row",m.row),
                                new SqlParameter("@kid",m.kid),
                                new SqlParameter("@answer",m.answer),
                                new SqlParameter("@result",m.result)
                                };
            return SqlHelper.RunProcedures("[ln_saveLearn]", pars);
        }
    }
}
