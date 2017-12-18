using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class pagingDAL : IDisposable
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
        ~pagingDAL() { Dispose(false); }
        #endregion
        /// <summary>
        /// 执行分页存储过程
        /// </summary>
        /// <param name="entity">分页实体</param>
        /// <param name="total">记录总数</param>
        /// <returns>DataTable</returns>
        public DataTable execPaging(Models.paging entity, out int total)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@viewName",SqlDbType.VarChar,800),
                                new SqlParameter("@fieldName",SqlDbType.VarChar,800),
                                new SqlParameter("@orderString",SqlDbType.VarChar,200),
                                new SqlParameter("@whereString",SqlDbType.VarChar,800),
                                new SqlParameter("@pageSize",SqlDbType.Int),
                                new SqlParameter("@pageNo",SqlDbType.Int),
                                new SqlParameter("@recordTotal",SqlDbType.Int)
                                };
            pars[0].Value = entity.table;
            pars[1].Value = entity.field;
            pars[2].Value = entity.order;
            pars[3].Value = entity.where;
            pars[4].Value = entity.pageSize;
            pars[5].Value = entity.pageNo;
            pars[6].Direction = ParameterDirection.Output;
            DataTable dt = SqlHelper.RunProcedure("[sp_paging]", pars);
            total = Convert.ToInt32(pars[6].Value);
            return dt;
        }
    }
}
