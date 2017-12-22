using System;
using System.Data.SqlClient;
using Models;

namespace DAL.root
{
    /// <summary>
    /// 课件数据访问类
    /// </summary>
    public class keJianDAL : IDisposable
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
        ~keJianDAL() { Dispose(false); }
        #endregion

        /// <summary>
        /// 添加课件
        /// </summary>
        /// <param name="objkeJian"></param>
        /// <returns></returns>
        public int AddKeJian(keJian objkeJian)
        {
            string sql = "insert into keJian(teacher, itid, title, cont, cont_typ, typ, xueshi, xueshi_minute,author)";
            sql += "values(@teacher, @itid, @title, @cont, @cont_typ, @typ, @xueshi, @xueshi_minute,@author)";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@teacher",objkeJian.teacher),
            new SqlParameter("@itid",objkeJian.itid),
            new SqlParameter("@title",objkeJian.title),
            new SqlParameter("@cont",objkeJian.cont),
            new SqlParameter("@cont_typ",objkeJian.cont_typ),
            new SqlParameter("@typ",objkeJian.typ),
            new SqlParameter("@xueshi",objkeJian.xueshi),
            new SqlParameter("@author",objkeJian.author!=null?objkeJian.author:""),
            new SqlParameter("@xueshi_minute",objkeJian.xueshi_minute)
            };
            return SqlHelper.ExcuteNonQuery(sql, para);
        }
        /// <summary>
        /// 根据id查询对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public keJian getEntity(int id)
        {
            string sql = "select id,teacher, itid, title,cont as curl, cont, cont_typ, typ, xueshi, xueshi_minute,author from keJian where id=@id";
            SqlParameter[] pars = { 
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteEntity<keJian>(sql, pars);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objkeJian"></param>
        /// <returns></returns>
        public int UpdateKeJian(keJian objkeJian)
        {
            string sql = "update keJian set teacher=@teacher, itid=@itid, title=@title, cont=@cont,";
            sql += " cont_typ=@cont_typ, typ=@typ, xueshi=@xueshi, xueshi_minute=@xueshi_minute,author=@author where id=@id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@teacher",objkeJian.teacher),
            new SqlParameter("@itid",objkeJian.itid),
            new SqlParameter("@title",objkeJian.title),
            new SqlParameter("@cont",objkeJian.cont),
            new SqlParameter("@cont_typ",objkeJian.cont_typ),
            new SqlParameter("@typ",objkeJian.typ),
            new SqlParameter("@xueshi",objkeJian.xueshi),
            new SqlParameter("@xueshi_minute",objkeJian.xueshi_minute),
            new SqlParameter("@author",objkeJian.author!=null?objkeJian.author:""),
            new SqlParameter("@id",objkeJian.id)
            };
            return SqlHelper.ExcuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Del(int id)
        {
            string sql = "delete from keJian where id=@id";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@id", id) };
            return SqlHelper.ExcuteNonQuery(sql, para);
        }
    }
}