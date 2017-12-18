/*
 * 创建人:   hllive
 * 创建时间: 2017/10/20 10:13:53
 * 描述:
 */
using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class departmentDAL : IDisposable
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
        ~departmentDAL() { Dispose(false); }
        #endregion

        public DataTable getSelect(int companyID, int pid)
        {
            string sql = "select id[val],name[text] from department where valid=1 and companyID=@companyID and pid=@pid";
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@companyID",companyID),
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, pars);
        }
        /// <summary>
        /// 保存部门或科室
        /// </summary>
        /// <param name="m"></param>
        public void save(department m)
        {
            string sql = "insert into department(companyID,name,pid,type,valid,must,isExam)values(@companyID,@name,@pid,@type,@valid,@must,@isExam)";
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@companyID",m.companyID),
                new SqlParameter("@name",m.name),
                new SqlParameter("@pid",m.pid),
                new SqlParameter("@type",m.type),
                new SqlParameter("@valid",m.valid),
                new SqlParameter("@must",m.must),
                new SqlParameter("@isExam",m.isExam)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void updateks(department m)
        {
            string sql = "update department set name=@name,pid=@pid,must=@must,valid=@valid,isExam=@isExam where ID=@ID";
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@name",m.name),
                new SqlParameter("@pid",m.pid),
                new SqlParameter("@must",m.must),
                new SqlParameter("@valid",m.valid),
                new SqlParameter("@isExam",m.isExam),
                new SqlParameter("@ID",m.id)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void delete(int ID)
        {
            string sql = "delete department where ID=@ID";
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@ID",ID)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        public void deletebm(int ID)
        {
            string sql = "delete department where ID=@ID or pid=@ID";
            SqlParameter[] pars = new SqlParameter[] 
            {
                new SqlParameter("@ID",ID)
            };
            SqlHelper.ExcuteNonQuery(sql, pars);
        }
        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public department getEntity(int id)
        {
            string sql = "select id,pid,name,must,isExam,valid from department where ID=@ID";
            SqlParameter[] pars = new SqlParameter[]{
                new SqlParameter("@ID",id)
            };
            return SqlHelper.ExecuteEntity<department>(sql, pars);
        }

        /// <summary>
        /// 组卷获得部门  用于组卷 获得参考对象
        /// </summary>20170401
        /// <param name="companyID"></param>
        /// <returns></returns>
        public DataTable get_zj_BM(int companyID)
        {
            string sql = "select ID,name,must,number from department where valid=1 and [type]=1 and isExam=1 and companyID=@companyID";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@companyID",companyID)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
        /// <summary>
        /// 组卷获得科室  用于组卷 获得参考对象
        /// </summary>20170401
        /// <param name="companyID"></param>
        /// <returns></returns>
        public DataTable get_zj_KS(int companyID, int pid)
        {
            string sql = "select ID,name,must,number from department where valid=1 and [type]=2 and isExam=1 and companyID=@companyID and pid=@pid";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@companyID",companyID),
                new SqlParameter("@pid",pid)
            };
            return SqlHelper.ExcuteDataTable(sql, param);
        }
    }
}
