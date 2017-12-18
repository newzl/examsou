/*
 * 创建人:   hllive
 * 创建时间: 2017/10/30 10:08:06
 * 描述:
 */
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class feedbackDAL : IDisposable
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
        ~feedbackDAL() { Dispose(false); }
        #endregion

        /// <summary>
        /// 保存意见反馈
        /// </summary>20170313
        /// <param name="eid"></param>
        /// <param name="content"></param>
        public void saveFeedback(string eid, string content)
        {
            string sql = "BEGIN TRAN insert into feedback(eid,content)values(@eid,@content) COMMIT TRAN";
            SqlParameter[] paras = {
				new SqlParameter("@eid", eid),
				new SqlParameter("@content", content)
		    };
            SqlHelper.ExcuteNonQuery(sql, paras);
        }
    }
}
