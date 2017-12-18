/*
 * 创建人:   hllive
 * 创建时间: 2017/11/7 11:26:36
 * 描述:
 */

namespace BLL.learn
{
    public class learnCollectBLL
    {
        /// <summary>
        /// 保存收藏
        /// </summary>
        /// <param name="m"></param>
        /// <returns>新插入ID</returns>
        public static int save(Models.learnCollect m)
        {
            using (DAL.learn.learnCollectDAL dal = new DAL.learn.learnCollectDAL())
            {
                if (m.cid > 0)
                {
                    return dal.delete(m.cid);
                }
                else
                {
                    return dal.save(m);
                }
            }
        }
    }
}
