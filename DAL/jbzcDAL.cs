/*
 * 创建人:   hllive
 * 创建时间: 2017/10/20 11:51:42
 * 描述:级别职称数据库操作
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class jbzcDAL : IDisposable
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
        ~jbzcDAL() { Dispose(false); }
        #endregion

        #region 公共获得级别职称
        //获得级别
        public DataTable getjb()
        {
            string sql = "select ID[val],name[text] from subjectLevel where ID<>0";
            return SqlHelper.ExcuteDataTable(sql, null);
        }
        public DataTable getAllJb()
        {
            string sql = "select ID[val],name[text] from subjectLevel";
            return SqlHelper.ExcuteDataTable(sql, null);
        }
        //获得职称
        public DataTable getzc(int companyID, int pid)
        {
            string sql = "select ID[val],jobName[text] from JobInfo where valid=1 and companyID=@companyID";
            SqlParameter[] param = null;
            if (pid != -1)
            {
                sql += " and levelID=@pid";
                param = new SqlParameter[] 
               {
                     new SqlParameter("@companyID",companyID),
                     new SqlParameter("@pid",pid)
                };
            }
            else
            {
                param = new SqlParameter[] { new SqlParameter("@companyID", companyID) };
            }

            return SqlHelper.ExcuteDataTable(sql, param);
        }
        #endregion

        #region 后台管理维护
        
        //保存职称
        public void savezc(Models.jobinfo m)
        {
            string sql = "insert into JobInfo(companyID,jobName,levelID,valid)values(@companyID,@jobName,@levelID,@valid)";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@companyID",m.companyID),
                new SqlParameter("@jobName",m.name),
                new SqlParameter("@levelID",m.jbid),
                new SqlParameter("@valid",m.valid)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        //修改职称
        public void updatezc(Models.jobinfo m)
        {
            string sql = "update JobInfo set jobName=@jobName,levelID=@levelID,valid=@valid where ID=@ID";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@jobName",m.name),
                new SqlParameter("@levelID",m.jbid),
                new SqlParameter("@valid",m.valid),
                new SqlParameter("@ID",m.id)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }
        //删除职称
        public void deletezc(int ID)
        {
            string sql = "delete JobInfo where ID=@ID";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ID",ID)
            };
            SqlHelper.ExcuteNonQuery(sql, param);
        }

        // 获得实体
        public Models.jobinfo getEntity(int id)
        {
            string sql = "select id,levelID[jbid],jobName[name],valid from JobInfo where ID=@ID";
            SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@ID",id)
            };
            return SqlHelper.ExecuteEntity<Models.jobinfo>(sql, pars);
        }
        #endregion

    }
}