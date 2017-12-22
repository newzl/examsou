using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL.root
{
    public class edu_teacherDAL : IDisposable
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
        ~edu_teacherDAL() { Dispose(false); }
        #endregion
        public int AddEdu_teacher(edu_teacher m)
        {
            string sql = "insert into edu_teacher(pic,name,zc,detail,isHome,valid)values(@pic,@name,@zc,@detail,@isHome,@valid)";
            SqlParameter[] param = new SqlParameter[]{ 
                       new SqlParameter("@pic",m.pic),
                       new SqlParameter("@name",m.name),
                       new SqlParameter("@zc",m.zc),
                       new SqlParameter("@detail",m.detail),
                       new SqlParameter("@isHome",m.isHome),
                       new SqlParameter("@valid",m.valid)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        public int UpdateKeJian(edu_teacher m)
        {
            string sql = "update edu_teacher set pic=@pic,name=@name,zc=@zc,detail=@detail,isHome=@isHome,valid=@valid where id=@id";
            SqlParameter[] param = new SqlParameter[]{ 
                       new SqlParameter("@pic",m.pic),
                       new SqlParameter("@name",m.name),
                       new SqlParameter("@zc",m.zc),
                       new SqlParameter("@detail",m.detail),
                       new SqlParameter("@isHome",m.isHome),
                       new SqlParameter("@valid",m.valid),
                       new SqlParameter("@id",m.id)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        public edu_teacher getTeacher(int id)
        {
            string sql = "select id,pic,name,zc,detail,isHome,valid from edu_teacher where id=@id";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@id", id) };
            return SqlHelper.ExecuteEntity<edu_teacher>(sql, param);
        }
        public DataTable getTeacher()
        {
            string sql = "select ID[val],name[text] from edu_teacher where isDel=0";
            return SqlHelper.ExcuteDataTable(sql);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTeacher(int id)
        {
            string sql = "update edu_teacher set isDel=1 where id=@id";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@id", id) };
            return SqlHelper.ExcuteScalre(sql, param);
        }
        /// <summary>
        /// 获取老师
        /// </summary>
        /// <returns></returns>
        public DataTable GetName()
        {
            string sql = "select id[val],name[text] from edu_teacher where isHome=1 and isDel=0";
            return SqlHelper.ExcuteDataTable(sql);
        }
    }
}
