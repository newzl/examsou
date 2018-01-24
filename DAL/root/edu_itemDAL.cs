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
            string sql = "select e.id,s.pId,e.bh,e.name,e.pic,e.typ,e.xsNumber,e.startDate,e.endDate,e.scidArr,e.useTime,e.passScore,e.scores,e.chs,e.chvs,e.mus,e.muvs,e.jus,e.juvs,e.xf,e.fzr,e.fzdw,e.detail,e.scid,e.isHome,e.valid from edu_item e inner join subjectClass s on e.scid=s.ID where e.id=@id ";
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
            string sql = "insert into edu_item(bh,name,pic,typ,xf,fzr,fzdw,detail,scid,isHome,valid,xsNumber,scidArr,startDate,endDate,scores,chs,chvs,mus,muvs,jus,juvs,useTime,passScore)";
            sql += " values(@bh,@name,@pic,@typ,@xf,@fzr,@fzdw,@detail,@scid,@isHome,@valid,@xsNumber,@scidArr,@startDate,@endDate,@scores,@chs,@chvs,@mus,@muvs,@jus,@juvs,@useTime,@passScore)";
            SqlParameter[] para = new SqlParameter[] 
            { 
                      new SqlParameter("@bh",objEdu_item.bh),
                      new SqlParameter("@name",objEdu_item.name),
                      new SqlParameter("@pic",objEdu_item.pic==null?"":objEdu_item.pic),
                      new SqlParameter("@typ",objEdu_item.typ),
                      new SqlParameter("@xf",objEdu_item.xf),
                      new SqlParameter("@fzr",objEdu_item.fzr),
                      new SqlParameter("@fzdw",objEdu_item.fzdw),
                      new SqlParameter("@xsNumber",objEdu_item.xsNumber),
                      new SqlParameter("@detail",objEdu_item.detail),
                      new SqlParameter("@scid",objEdu_item.scid),
                      new SqlParameter("@isHome",objEdu_item.isHome),
                      new SqlParameter("@scidArr",objEdu_item.scidArr),
                      new SqlParameter("@startDate",objEdu_item.startDate),
                      new SqlParameter("@endDate",objEdu_item.endDate), 
                      new SqlParameter("@valid",objEdu_item.valid),
                      new SqlParameter("@scores",objEdu_item.scores),
                      new SqlParameter("@chs",objEdu_item.chs),
                      new SqlParameter("@chvs",objEdu_item.chvs),
                      new SqlParameter("@mus",objEdu_item.mus),
                      new SqlParameter("@muvs",objEdu_item.muvs),
                      new SqlParameter("@jus",objEdu_item.jus),
                      new SqlParameter("@juvs",objEdu_item.juvs),
                      new SqlParameter("@passScore",objEdu_item.passScore),
                      new SqlParameter("@useTime",objEdu_item.useTime),
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
            string sql = "update edu_item set bh=@bh,name=@name,pic=@pic,typ=@typ,xf=@xf,fzr=@fzr,scores=@scores,chs=@chs,chvs=@chvs,mus=@mus,muvs=@muvs,jus=@jus,juvs=@juvs,useTime=@useTime,passScore=@passScore,";
            sql += " fzdw=@fzdw,detail=@detail,scid=@scid,isHome=@isHome,valid=@valid,xsNumber=@xsNumber,scidArr=@scidArr,startDate=@startDate,endDate=@endDate where id=@id ";
            SqlParameter[] para = new SqlParameter[] 
            { 
                      new SqlParameter("@id",objEdu_item.id),
                      new SqlParameter("@bh",objEdu_item.bh),
                      new SqlParameter("@name",objEdu_item.name),
                      new SqlParameter("@pic",objEdu_item.pic==null?"":objEdu_item.pic),
                      new SqlParameter("@typ",objEdu_item.typ),
                      new SqlParameter("@xf",objEdu_item.xf),
                      new SqlParameter("@fzr",objEdu_item.fzr),
                      new SqlParameter("@scores",objEdu_item.scores),
                      new SqlParameter("@chs",objEdu_item.chs),
                      new SqlParameter("@chvs",objEdu_item.chvs),
                      new SqlParameter("@mus",objEdu_item.mus),
                      new SqlParameter("@muvs",objEdu_item.muvs),
                      new SqlParameter("@jus",objEdu_item.jus),
                      new SqlParameter("@juvs",objEdu_item.juvs),
                      new SqlParameter("@passScore",objEdu_item.passScore),
                      new SqlParameter("@useTime",objEdu_item.useTime),
                      new SqlParameter("@xsNumber",objEdu_item.xsNumber), 
                      new SqlParameter("@fzdw",objEdu_item.fzdw),
                      new SqlParameter("@detail",objEdu_item.detail),
                      new SqlParameter("@scid",objEdu_item.scid),
                      new SqlParameter("@isHome",objEdu_item.isHome),
                      new SqlParameter("@scidArr",objEdu_item.scidArr),
                      new SqlParameter("@startDate",objEdu_item.startDate),
                      new SqlParameter("@endDate",objEdu_item.endDate),
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
            if (new keJianDAL().Exists(id) == 0)
            {
                string sql = "update edu_item set isDel=1 where id=@id";
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@id", id) };
                return SqlHelper.ExcuteScalre(sql, param);
            }
            else
            {
                return -1;
            }
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
        /// <summary>
        /// 查看继教项目详细信息
        /// </summary>
        /// <param name="itid"></param>
        /// <returns></returns>
        public DataSet detail(int itid)
        {
            string sql = "select pic,a.name,b.name[itype],xf,bh,fzr,fzdw,mustTime,scores,passScore,useTime,CONVERT(varchar(16),startDate,25) + ' 至 ' + CONVERT(varchar(16),endDate,25)[qzrq],a.detail from edu_item a left join vp_itemTyp b on a.typ=b.id where a.id=@itid";
            sql += " select title,b.name[typ],case when c.name is not null then c.name else a.author end[teacher],xueshi[xs],xueshi_minute[sc] from keJian a left join vp_learnTyp b on a.typ=b.id left join edu_teacher c on a.teacher=c.id where a.itid=@itid";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@itid", itid) };
            return SqlHelper.ExcuteDataSet(sql, param);
        }
    }
}
