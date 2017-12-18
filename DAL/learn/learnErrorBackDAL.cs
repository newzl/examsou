/*
 * 创建人:   hllive
 * 创建时间: 2017/11/13 15:18:18
 * 描述:
 */
using System;
using System.Data.SqlClient;

namespace DAL.learn
{
    public class learnErrorBackDAL : IDisposable
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
        ~learnErrorBackDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 保存错题反馈
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public void save(Models.learnErrorBack m)
        {
            string sql = "insert into learnErrorBack([uid],stype,kid,errType,content)values(@uid,@stype,@kid,@errType,@content)";
            SqlParameter[] pars = {
                                new SqlParameter("@uid",m.uid),
                                new SqlParameter("@stype",m.stype),
                                new SqlParameter("@kid",m.kid),
                                new SqlParameter("@errType",m.errType),
                                new SqlParameter("@content",m.content)
                                };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
    }
}
