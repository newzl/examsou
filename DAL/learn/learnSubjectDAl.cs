using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class learnSubjectDAl : IDisposable
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
        ~learnSubjectDAl() { Dispose(false); }
        #endregion

        /// <summary>
        /// 保存学习题库,保存成功  题库学习人数加一
        /// </summary>
        /// <param name="eid">用户ID</param>
        /// <param name="sid">题库ID</param>
        /// <returns></returns>
        public int saveLearnSubject(string eid, int sid)
        {
            string strSQL = "if not exists(select id from learnSubject where [eid]=@eid and [sid]=@sid) begin insert learnSubject(eid,sid) values(@eid,@sid);update subjectClass set learns=learns+1 where ID=@sid end";
            SqlParameter[] pars = {
                                new SqlParameter("@eid",eid),
                                new SqlParameter("@sid",sid)
                                };
            return SqlHelper.ExcuteNonQuery(strSQL, pars);
        }
        /// <summary>
        /// 取消或清空学习学习  查看存储过程[ln_cancelLearn]
        /// </summary>
        /// <param name="lid">learnSubject表ID</param>
        /// <returns></returns>
        public void cancelLearnSubject(int lid, string eid, string type)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@lid",lid),
                                new SqlParameter("@eid",eid),
                                new SqlParameter("@type",type)
                                };
            SqlHelper.RunProcedure("[ln_cancelLearn]", pars);
        }
        /// <summary>
        /// 开始学习
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="eid"></param>
        public void beginLearn(int lid, string eid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@lid",lid),
                                new SqlParameter("@eid",eid)
                                };
            SqlHelper.RunProcedure("ln_beginLearn", pars);
        }
        /// <summary>
        /// 我的学习首页
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public learnIndex learnIndex(string eid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@eid",eid)
                                };
            return SqlHelper.RunProcedureEntity<learnIndex>("ln_index", pars);
        }
        /// <summary>
        /// 通过eid获得正在学习的题库
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public inlearn getInlearn(string eid)
        {
            string sql = "select a.id[lid],a.[sid],b.name[sname] from learnSubject a left join subjectClass b on a.[sid]=b.ID where inlearn=1 and eid=@eid";
            SqlParameter[] pars = {
                                new SqlParameter("@eid",eid)
                                };
            return SqlHelper.ExecuteEntity<inlearn>(sql, pars);
        }
        /// <summary>
        /// 章节练习获得数据
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="scid"></param>
        /// <returns></returns>
        public DataTable getChapter(string eid, int scid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@eid",eid),
                                new SqlParameter("@scid",scid)
                                };
            return SqlHelper.RunProcedure("[ln_chapter]", pars);
        }
        /// <summary>
        /// 我的题库list，通过eid获得我的题库table
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public DataTable getMyLearnTab(string eid)
        {
            string sql = "select id,[sid],sname,[level],createTime,learns,counts,total,inlearn from v_learnSubject where eid=@eid order by createTime desc";
            SqlParameter[] pars = {
                                new SqlParameter("@eid",eid)
                                };
            return SqlHelper.ExcuteDataTable(sql, pars);
        }
    }
}
