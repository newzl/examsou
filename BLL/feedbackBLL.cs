/*
 * 创建人:   hllive
 * 创建时间: 2017/10/30 10:10:23
 * 描述:
 */

namespace BLL
{
    public class feedbackBLL
    {
        /// <summary>
        /// 保存反馈信息
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="content"></param>
        public static void saveFeedback(string eid, string content) {
            using (DAL.feedbackDAL dal=new DAL.feedbackDAL())
            {
                dal.saveFeedback(eid, content);
            }
        }
    }
}
