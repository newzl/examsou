using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL.root
{
    public class edu_itemDAL : IDisposable
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
        ~edu_itemDAL() { Dispose(false); }
        #endregion
        public DataTable GetTyp()
        {
            string sql = "select ID[val],name[text] from  vp_itemTyp ";
            return SqlHelper.ExcuteDataTable(sql);
        }
    }
}
