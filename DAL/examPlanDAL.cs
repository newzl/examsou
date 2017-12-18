/*
 * 创建人:   hllive
 * 创建时间: 2017/12/7 15:10:41
 * 描述:
 */
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class examPlanDAL : IDisposable
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
        ~examPlanDAL() { Dispose(false); }
        #endregion

        #region 后台
        #region 保存和修改组卷
        /// <summary>
        /// 保存随机组卷
        /// </summary>
        /// <param name="entity"></param>
        public void save_sjzj(Models.examPlan m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@companyID",m.companyID),
                new SqlParameter("@name",m.name),
                new SqlParameter("@explain",m.explain),
                new SqlParameter("@sidList",m.sidList),
                new SqlParameter("@chs",m.chs),
                new SqlParameter("@chvs",m.chvs),
                new SqlParameter("@mus",m.mus),
                new SqlParameter("@muvs",m.muvs),
                new SqlParameter("@jus",m.jus),
                new SqlParameter("@juvs",m.juvs),
                new SqlParameter("@passScore",m.passScore),
                new SqlParameter("@examNum",m.examNum),
                new SqlParameter("@useTime",m.useTime),
                new SqlParameter("@isLimit",m.isLimit),
                new SqlParameter("@startTime",m.startTime),
                new SqlParameter("@endTime",m.endTime),
                new SqlParameter("@isShow",m.isShow),
                new SqlParameter("@isCopy",m.isCopy),
                new SqlParameter("@state",m.state),
                new SqlParameter("@creator",m.creator),
                new SqlParameter("@examJoin",m.examJoin)
            };
            SqlHelper.RunProcedure("[zj_save_sjzj]", pars);
        }
        /// <summary>
        /// 修改随机组卷
        /// </summary>
        /// <param name="entity"></param>
        public void update_sjzj(Models.examPlan m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@name",m.name),
                new SqlParameter("@explain",m.explain),
                new SqlParameter("@sidList",m.sidList),
                new SqlParameter("@chs",m.chs),
                new SqlParameter("@chvs",m.chvs),
                new SqlParameter("@mus",m.mus),
                new SqlParameter("@muvs",m.muvs),
                new SqlParameter("@jus",m.jus),
                new SqlParameter("@juvs",m.juvs),
                new SqlParameter("@passScore",m.passScore),
                new SqlParameter("@examNum",m.examNum),
                new SqlParameter("@useTime",m.useTime),
                new SqlParameter("@isLimit",m.isLimit),
                new SqlParameter("@startTime",m.startTime),
                new SqlParameter("@endTime",m.endTime),
                new SqlParameter("@isShow",m.isShow),
                new SqlParameter("@isCopy",m.isCopy),
                new SqlParameter("@state",m.state),
                new SqlParameter("@examJoin",m.examJoin),
                new SqlParameter("@epid",m.id)
            };
            SqlHelper.RunProcedure("[zj_update_sjzj]", pars);
        }
        /// <summary>
        /// 保存手工组卷
        /// </summary>
        /// <param name="entity"></param>
        public void save_sgzj(Models.examPlan m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@companyID",m.companyID),
                new SqlParameter("@name",m.name),
                new SqlParameter("@explain",m.explain),
                new SqlParameter("@chlist",m.chlist),
                new SqlParameter("@mulist",m.mulist),
                new SqlParameter("@julist",m.julist),
                new SqlParameter("@chs",m.chs),
                new SqlParameter("@chvs",m.chvs),
                new SqlParameter("@mus",m.mus),
                new SqlParameter("@muvs",m.muvs),
                new SqlParameter("@jus",m.jus),
                new SqlParameter("@juvs",m.juvs),
                new SqlParameter("@passScore",m.passScore),
                new SqlParameter("@examNum",m.examNum),
                new SqlParameter("@useTime",m.useTime),
                new SqlParameter("@isLimit",m.isLimit),
                new SqlParameter("@startTime",m.startTime),
                new SqlParameter("@endTime",m.endTime),
                new SqlParameter("@isShow",m.isShow),
                new SqlParameter("@isCopy",m.isCopy),
                new SqlParameter("@state",m.state),
                new SqlParameter("@creator",m.creator),
                new SqlParameter("@examJoin",m.examJoin)
            };
            SqlHelper.RunProcedures("[zj_save_sgzj]", pars);
        }
        /// <summary>
        /// 修改手工组卷
        /// </summary>
        /// <param name="entity"></param>
        public void update_sgzj(Models.examPlan m)
        {
            SqlParameter[] pars = {
                new SqlParameter("@name",m.name),
                new SqlParameter("@explain",m.explain),
                new SqlParameter("@chlist",m.chlist),
                new SqlParameter("@mulist",m.mulist),
                new SqlParameter("@julist",m.julist),
                new SqlParameter("@chs",m.chs),
                new SqlParameter("@chvs",m.chvs),
                new SqlParameter("@mus",m.mus),
                new SqlParameter("@muvs",m.muvs),
                new SqlParameter("@jus",m.jus),
                new SqlParameter("@juvs",m.juvs),
                new SqlParameter("@passScore",m.passScore),
                new SqlParameter("@examNum",m.examNum),
                new SqlParameter("@useTime",m.useTime),
                new SqlParameter("@isLimit",m.isLimit),
                new SqlParameter("@startTime",m.startTime),
                new SqlParameter("@endTime",m.endTime),
                new SqlParameter("@isShow",m.isShow),
                new SqlParameter("@isCopy",m.isCopy),
                new SqlParameter("@state",m.state),
                new SqlParameter("@examJoin",m.examJoin),
                new SqlParameter("@epid",m.id)
            };
            SqlHelper.RunProcedure("[zj_update_sgzj]", pars);
        }
        #endregion

        #region get_zj
        /// <summary>
        /// 获得随机组卷-用于修改
        /// </summary>
        /// <param name="epid"></param>
        /// <returns></returns>
        public DataTable get_sjzj(int epid) {
            SqlParameter[] pars = {
               new SqlParameter("@epid",epid)
            };
            return SqlHelper.RunProcedure("[zj_get_sjzj]", pars);
        }
        /// <summary>
        /// 获得手工组卷-用于修改
        /// </summary>
        /// <param name="epid"></param>
        /// <returns></returns>
        public DataTable get_sgzj(int epid)
        {
            SqlParameter[] pars = {
               new SqlParameter("@epid",epid)
            };
            return SqlHelper.RunProcedure("[jz_get_sgzj]", pars);
        }
        #endregion

        #region show_zj
        //显示随机组卷
        public DataSet show_sjzj(int epid)
        {
            SqlParameter[] pars = {
               new SqlParameter("@epid",epid)
            };
            return SqlHelper.RunProcedure("[zj_show_sjzj]", pars, "show_sjzj");
        }
        //显示手工组卷info
        public DataSet show_sgzj(int epid, string type)
        {
            SqlParameter[] pars = {
                new SqlParameter("@epid",epid),
                new SqlParameter("@type",type)
            };
            return SqlHelper.RunProcedure("[zj_show_sgzj]", pars, "show_sgzj");
        }
        #endregion

        //删除组卷（标记删除）
        public void delPlan(int epid)
        {
            string strSQL = "update examPlan set valid=0 where id=@epid";
            SqlParameter[] pars = {
                new SqlParameter("@epid",epid)
            };
            SqlHelper.ExcuteNonQuery(strSQL, pars);
        }
        #endregion

        #region 前台
        //准备考试table详细
        public DataTable showPlan(int epid, string eid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@epid",epid),
                new SqlParameter("@eid",eid)
            };
            return SqlHelper.RunProcedure("[exs_showPlan]", pars);
        }
        /// <summary>
        /// 判断是否能考试
        /// </summary>有记录 表示已经参考 返回true，没有记录表示没有参考 返回false
        /// <param name="epid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public bool isJoinExam(int epid, string eid)
        {
            SqlParameter[] pars = {
                                new SqlParameter("@epid",SqlDbType.Int),
                                new SqlParameter("@eid",SqlDbType.VarChar,36),
                                new SqlParameter("@isExam",SqlDbType.Bit),
                                };
            pars[0].Value = epid;
            pars[1].Value = eid;
            pars[2].Direction = ParameterDirection.Output;
            SqlHelper.RunProcedure("[exs_isJoinExam]", pars);
            return Convert.ToBoolean(pars[2].Value);
        }
        //获得考试试卷
        public DataSet getExamPlanPaper(int epid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@epid",epid)
            };
            return SqlHelper.RunProcedure("[exs_getExamPlanPaper]", pars, "examPlanPaper");
        }
        /// <summary>
        /// 查看考试试卷
        /// </summary>
        public DataSet showExamPlan(int erid)
        {
            SqlParameter[] pars = {
                new SqlParameter("@erid",erid)
            };
            return SqlHelper.RunProcedure("[exs_showExamPlan]", pars, "showExamPlan");
        }
        #endregion

    }
}
