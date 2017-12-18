/*
 * 创建人:   hllive
 * 创建时间: 2017/10/23 16:11:16
 * 描述:
 */
using System.Data;

namespace BLL
{
    public class reportBLL
    {
        /// <summary>
        /// 考试情况统计
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="passScore"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public static string examReport(string TimeStart, string TimeEnd, decimal passScore, string whereStr)
        {
            using (DAL.reportDAL dal = new DAL.reportDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    DataTable dt = dal.examReport(TimeStart, TimeEnd, passScore, whereStr);
                    return jp.ToLayuiTable(dt, dt.Rows.Count);
                }
            }
        }
        //学习情况统计
        public static string learnReport(string TimeStart, string TimeEnd, int companyID, string whereStr)
        {
            using (DAL.reportDAL dal = new DAL.reportDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    DataTable dt = dal.learnReport(TimeStart, TimeEnd, companyID, whereStr);
                    return jp.ToLayuiTable(dt, dt.Rows.Count);
                }
            }
        }

        /// <summary>
        /// 注册统计
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static string registerReport(string whereStr)
        {
            using (DAL.reportDAL dal = new DAL.reportDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    DataTable dt = dal.registerReport(whereStr);
                    return jp.ToLayuiTable(dt, dt.Rows.Count);
                }
            }
        }
        /// <summary>
        /// 职称注册情况
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static string reportJob(int companyID)
        {
            using (DAL.reportDAL dal = new DAL.reportDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    DataTable dt = dal.reportJob(companyID);
                    return jp.ToLayuiTable(dt, dt.Rows.Count);
                }
            }
        }

    }
}
