/*
 * 创建人:   hllive
 * 创建时间: 2017/11/10 14:57:00
 * 描述:
 */

using System.Data;
namespace BLL.learn
{
    public class learnsxBLL
    {
        /// <summary>
        /// 顺序练习执行[ln_learnsx]存储过程
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string exeLearnsx(Models.learnsx m)
        {
            using (DAL.learn.learnsxDAL dal = new DAL.learn.learnsxDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.exeLearnsx(m));
                }
            }
        }
        /// <summary>
        /// 获得快速背题
        /// </summary>
        /// <param name="m"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string exeLearnks(Models.learnks m)
        {
            using (DAL.learn.learnsxDAL dal = new DAL.learn.learnsxDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    int count = 0;
                    DataTable dt = dal.exeLearnks(m, out count);
                    return jp.ToTablePage(dt, count);
                }
            }
        }
        /// <summary>
        /// 保存学习
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>

        public static int exeSaveLearn(Models.saveLearn m)
        {
            using (DAL.learn.learnsxDAL dal = new DAL.learn.learnsxDAL())
            {
                return dal.exeSaveLearn(m);
            }
        }
    }
}
