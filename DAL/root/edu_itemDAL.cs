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

        /// <summary>
        /// 根据id获取项目对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public edu_item ByIdEduItem(int id)
        {
            string sql = "select e.id,s.pId,e.bh,e.name,e.pic,e.typ,e.xf,e.fzr,e.fzdw,e.detail,e.scid,e.isHome,e.valid from edu_item e inner join subjectClass s on e.scid=s.ID where e.id=@id ";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@id", id) };
            return SqlHelper.ExecuteEntity<edu_item>(sql, para);
        }

        /// <summary>
        ///项目类型 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTyp()
        {
            string sql = "select ID[val],name[text] from  vp_itemTyp ";
            return SqlHelper.ExcuteDataTable(sql);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="objEdu_item"></param>
        /// <returns></returns>
        public int AddEduItem(edu_item objEdu_item)
        {
            string sql = "insert into edu_item(bh,name,pic,typ,xf,fzr,fzdw,detail,scid,isHome,valid)";
            sql += " values(@bh,@name,@pic,@typ,@xf,@fzr,@fzdw,@detail,@scid,@isHome,@valid)";
            SqlParameter[] para = new SqlParameter[] 
            { 
                      new SqlParameter("@bh",objEdu_item.bh),
                      new SqlParameter("@name",objEdu_item.name),
                      new SqlParameter("@pic",objEdu_item.pic==null?"":objEdu_item.pic),
                      new SqlParameter("@typ",objEdu_item.typ),
                      new SqlParameter("@xf",objEdu_item.xf),
                      new SqlParameter("@fzr",objEdu_item.fzr),
                      new SqlParameter("@fzdw",objEdu_item.fzdw),
                      //new SqlParameter("@teacher",objEdu_item.teacher),
                      new SqlParameter("@detail",objEdu_item.detail),
                      new SqlParameter("@scid",objEdu_item.scid),
                      new SqlParameter("@isHome",objEdu_item.isHome),
                      new SqlParameter("@valid",objEdu_item.valid)
            };
            return SqlHelper.ExcuteNonQuery(sql, para);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objEdu_item"></param>
        /// <returns></returns>
        public int UpdateEduItem(edu_item objEdu_item)
        {
            string sql = "update edu_item set bh=@bh,name=@name,pic=@pic,typ=@typ,xf=@xf,fzr=@fzr,";
            sql += " fzdw=@fzdw,detail=@detail,scid=@scid,isHome=@isHome,valid=@valid where id=@id ";
            SqlParameter[] para = new SqlParameter[] 
            { 
                      new SqlParameter("@id",objEdu_item.id),
                      new SqlParameter("@bh",objEdu_item.bh),
                      new SqlParameter("@name",objEdu_item.name),
                      new SqlParameter("@pic",objEdu_item.pic==null?"":objEdu_item.pic),
                      new SqlParameter("@typ",objEdu_item.typ),
                      new SqlParameter("@xf",objEdu_item.xf),
                      new SqlParameter("@fzr",objEdu_item.fzr),
                      new SqlParameter("@fzdw",objEdu_item.fzdw),
                      //new SqlParameter("@teacher",objEdu_item.teacher),
                      new SqlParameter("@detail",objEdu_item.detail),
                      new SqlParameter("@scid",objEdu_item.scid),
                      new SqlParameter("@isHome",objEdu_item.isHome),
                      new SqlParameter("@valid",objEdu_item.valid)
            };
            return SqlHelper.ExcuteNonQuery(sql, para);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelEduItem(int id)
        {
            string sql = "update edu_item set isDel=1 where id=@id";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@id", id) };
            return SqlHelper.ExcuteScalre(sql, param);
        }
        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <returns></returns>
        public DataTable GetName()
        {
            string sql = "select id[val],name[text] from edu_item where isHome=1 and isDel=0";
            return SqlHelper.ExcuteDataTable(sql);
        }
    }
}
