/*
 * 创建人:   hllive
 * 创建时间: 2017/11/7 10:55:47
 * 描述:学习收藏-数据库操作
 */
using System;
using System.Data.SqlClient;

namespace DAL.learn
{
    public class learnCollectDAL : IDisposable
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
        ~learnCollectDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 保存收藏，返回插入ID
        /// </summary>
        /// <param name="m"></param>
        /// <returns>新插入ID</returns>
        public int save(Models.learnCollect m)
        {
            string sql = "if not exists(select * from learnCollect where miid=@miid and stype=@stype and kid=@kid) begin insert into learnCollect(miid,stype,kid)values(@miid,@stype,@kid)select @@IDENTITY end";
            SqlParameter[] pars = {
                new SqlParameter("@miid",m.miid),
                new SqlParameter("@stype",m.stype),
                new SqlParameter("@kid",m.kid)
            };
            return SqlHelper.ExcuteScalre(sql, pars);
        }
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="cid"></param>
        public int delete(int id)
        {
            string sql = "delete learnCollect where id=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExcuteNonQuery(sql, pars);
        }
    }
}
