/*
 * 创建人:   hllive
 * 创建时间: 2017/10/25 9:47:24
 * 描述:
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class publicDAL : IDisposable
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
        ~publicDAL() { Dispose(false); }
        #endregion

        /// <summary>
        /// 获得c_bm表里数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable getcbm(int type)
        {
            string sql = "select type_bm[val],name[text] from c_bm where [type]=@type";
            SqlParameter[] pars = { 
                new SqlParameter("@type", type)
            };
            return SqlHelper.ExcuteDataTable(sql, pars);
        }

        public DataTable getxzbm(string pxzbm)
        {
            string sql = "select xzbm[val],name[text] from vp_xzbm where pxzbm=@pxzbm";
            SqlParameter[] pars = {
                new SqlParameter("@pxzbm", pxzbm)
            };
            return SqlHelper.ExcuteDataTable(sql, pars);
        }
    }
}
