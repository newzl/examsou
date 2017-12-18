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
        public Models.learnInfo getLearnBaseInfo(int lid, int sid, string stype)
        {
            string sql = "select name[sname],chs,mus,jus,fis,qas,lss,mcs,[rows] from subjectClass a left join(select [sid],[rows] from learnHistory where lid=@lid and [sid]=@sid and stype=@stype) b on b.[sid]=a.ID where a.ID=@sid";
            SqlParameter[] pars = {
                                new SqlParameter("@lid",lid),
                                new SqlParameter("@sid",sid),
                                new SqlParameter("@stype",stype)
                                };
            return SqlHelper.ExecuteEntity<Models.learnInfo>(sql, pars);
        }
    }
}
