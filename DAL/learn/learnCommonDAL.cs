/*
 * 创建人:   hllive
 * 创建时间: 2017/11/8 17:12:00
 * 描述:
 */
using System;
using System.Data.SqlClient;

namespace DAL.learn
{
    public class learnCommonDAL : IDisposable
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
        ~learnCommonDAL() { Dispose(false); }
        #endregion
        //获得学习基本信息
        public Models.learnInfo getLearnBaseInfo(int miid, int scid, string stype)
        {
            string sql = "select chs,mus,jus,fis,qas,mcs,[rows],CONVERT(varchar(16),mendTime,25)[time] from subjectClass a left join(select * from learnHistory where miid=@miid and scid=@scid and stype=@stype) b on b.scid=a.ID where a.ID=@scid";
            SqlParameter[] pars = {
                                new SqlParameter("@miid",miid),
                                new SqlParameter("@scid",scid),
                                new SqlParameter("@stype",stype)
                                };
            return SqlHelper.ExecuteEntity<Models.learnInfo>(sql, pars);
        }
    }
}
