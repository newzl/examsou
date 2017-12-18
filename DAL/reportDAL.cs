/*
 * 创建人:   hllive
 * 创建时间: 2017/10/23 16:00:37
 * 描述:
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class reportDAL : IDisposable
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
        ~reportDAL() { Dispose(false); }
        #endregion

        //考试情况统计
        public DataTable examReport(string TimeStart, string TimeEnd, decimal passScore, string whereStr)
        {
            string strSQL = "select name,name2,number,yk,sk,wk,kscs,ksl,hgl from f_examrport(@TimeStart,@TimeEnd,@passScore)";
            if (!string.IsNullOrEmpty(whereStr)) strSQL += " where " + whereStr;
            SqlParameter[] pars = { 
                new SqlParameter("@TimeStart", TimeStart),
                new SqlParameter("@TimeEnd", TimeEnd),
                new SqlParameter("@passScore", passScore)
            };
            return SqlHelper.ExcuteDataTable(strSQL, pars);
        }
        //学习情况统计
        public DataTable learnReport(string TimeStart, string TimeEnd, int companyID, string whereStr)
        {
            string strSQL = "select name,name2,must,number,xxrs,xxcs,wx,xxl from f_learnReport(@TimeStart,@TimeEnd,@companyID)";
            if (!string.IsNullOrEmpty(whereStr)) strSQL += " where " + whereStr;
            SqlParameter[] param = {
                new SqlParameter("@TimeStart", TimeStart),
                new SqlParameter("@TimeEnd", TimeEnd),
                new SqlParameter("@companyID", companyID),
            };
            return SqlHelper.ExcuteDataTable(strSQL, param);
        }
        //注册情况统计
        public DataTable registerReport(string whereStr)
        {
            string strSQL = "select name,name2,must,zcrs,rzrs,zcl from v_registerReport";
            if (!string.IsNullOrEmpty(whereStr))
            {
                strSQL += " where " + whereStr;
            }
            return SqlHelper.ExcuteDataTable(strSQL, null);
        }
        //职称注册情况统计
        public DataTable reportJob(int companyID)
        {
            string strSQL = "select name1,name2,zcrs,rzrs from [v_reportJob] where companyID=@companyID order by id1";
            SqlParameter[] pars = { 
                new SqlParameter("@companyID", companyID)
            };
            return SqlHelper.ExcuteDataTable(strSQL, pars);
        }
    }
}
