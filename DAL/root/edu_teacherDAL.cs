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
        public int AddEdu_teacher(edu_teacher objedu_teacher)
        {
            string sql = "insert into edu_teacher(pic,name,zc,detail,isHome,valid)values(@pic,@name,@zc,@detail,@isHome,@valid)";
            SqlParameter[] param = new SqlParameter[]{ 
                       new SqlParameter("@pic",""),
                       new SqlParameter("@name",objedu_teacher.name),
                       new SqlParameter("@zc",objedu_teacher.zc),
                       new SqlParameter("@detail",objedu_teacher.detail),
                       new SqlParameter("@isHome",objedu_teacher.isHome),
                       new SqlParameter("@valid",objedu_teacher.valid)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        public int UpdateKeJian(edu_teacher objedu_teacher)
        {
            string sql = "update edu_teacher set pic=@pic,name=@name,zc=@zc,detail=@detail,isHome=@isHome,valid=@valid where id=@id";
            SqlParameter[] param = new SqlParameter[]{ 
                       new SqlParameter("@pic",""),
                       new SqlParameter("@name",objedu_teacher.name),
                       new SqlParameter("@zc",objedu_teacher.zc),
                       new SqlParameter("@detail",objedu_teacher.detail),
                       new SqlParameter("@isHome",objedu_teacher.isHome),
                       new SqlParameter("@valid",objedu_teacher.valid),
                       new SqlParameter("@id",objedu_teacher.id)
            };
            return SqlHelper.ExcuteNonQuery(sql, param);
        }
        public edu_teacher getTeacher(int id)
        {
            string sql = "select id,pic,name,zc,detail,isHome,valid  from edu_teacher where id=@id";
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
