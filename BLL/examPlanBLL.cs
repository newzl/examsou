using Models;
/*
 * 创建人:   hllive
 * 创建时间: 2017/12/7 16:50:18
 * 描述:
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class examPlanBLL
    {
        #region 保存和修改组卷
        //随机
        public static void save_sjzj(Models.examPlan m)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                dal.save_sjzj(m);
            }
        }
        public static void update_sjzj(Models.examPlan m)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                dal.update_sjzj(m);
            }
        }
        //手工
        public static void save_sgzj(Models.examPlan m)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                dal.save_sgzj(m);
            }
        }
        public static void update_sgzj(Models.examPlan m)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                dal.update_sgzj(m);
            }
        }
        #endregion
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="epid"></param>
        public static void delete_plan(int epid)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                dal.delPlan(epid);
            }
        }
        /// <summary>
        /// 查看随机组卷
        /// </summary>
        /// <param name="epid"></param>
        public static showsjzj show_sjzj(int epid)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                DataSet ds = dal.show_sjzj(epid);
                using (Common.DataParse dp = new Common.DataParse())
                {
                    showsjzj m = dp.DataRowToEntity<showsjzj>(ds.Tables[0].Rows[0]);
                    m.zjsubclss = dp.DataTableToList<zjsubclss>(ds.Tables[1]);
                    m.examJoin = dp.DataTableToList<examJoin>(ds.Tables[2]);
                    return m;
                }
            }
        }
        /// <summary>
        /// 查看手工组卷
        /// </summary>
        /// <param name="epid"></param>
        public static showsgzj show_sgzj(int epid)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                DataSet ds = dal.show_sgzj(epid, "showInfo");
                using (Common.DataParse dp = new Common.DataParse())
                {
                    showsgzj m = dp.DataRowToEntity<showsgzj>(ds.Tables[0].Rows[0]);
                    m.examJoin = dp.DataTableToList<examJoin>(ds.Tables[1]);
                    return m;
                }
            }
        }
        /// <summary>
        /// 导出试题-获得试题
        /// </summary>
        /// <param name="epid"></param>
        /// <returns></returns>
        public static exportPaper export(int epid)
        {
            using (DAL.examPlanDAL dal = new DAL.examPlanDAL())
            {
                DataSet ds = dal.show_sgzj(epid, "showSubject");
                using (Common.DataParse dp = new Common.DataParse())
                {
                    exportPaper m = dp.DataRowToEntity<exportPaper>(ds.Tables[0].Rows[0]);
                    m.dan = dp.DataTableToList<dan>(ds.Tables[1]);
                    m.duo = dp.DataTableToList<duo>(ds.Tables[2]);
                    m.pan = dp.DataTableToList<pan>(ds.Tables[3]);
                    return m;
                }
            }
        }
    }
}
