/*
 * 创建人:   hllive
 * 创建时间: 2017/11/1 16:10:11
 * 描述:
 */

namespace BLL
{
    public class commentBLL
    {
        /// <summary>
        /// 保存评论
        /// </summary>
        /// <param name="m"></param>
        public static void save(Models.comment m)
        {
            using (DAL.commentDAL dal = new DAL.commentDAL())
            {
                dal.save(m);
            }
        }
    }
}
