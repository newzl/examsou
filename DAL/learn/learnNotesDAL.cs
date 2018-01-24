/*
 * 创建人:   hllive
 * 创建时间: 2017/11/13 15:24:33
 * 描述:
 */
using System;
using System.Data.SqlClient;

namespace DAL.learn
{
    public class learnNotesDAL : IDisposable
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
        ~learnNotesDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 保存错题反馈
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public void save(Models.learnNotes m)
        {
            string sql = "if exists(select * from learnNotes where miid=@miid and stype=@stype and kid=@kid)";
            sql += " begin update learnNotes set content=@content,createTime=GETDATE() where miid=@miid and stype=@stype and kid=@kid end";
            sql += " else begin insert into learnNotes(miid,stype,kid,content)values(@miid,@stype,@kid,@content) end";
            SqlParameter[] pars = {
                new SqlParameter("@miid",m.miid),
                new SqlParameter("@stype",m.stype),
                new SqlParameter("@kid",m.kid),
                new SqlParameter("@content",m.content)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
    }
}
