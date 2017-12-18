/*
 * 创建人:   hllive
 * 创建时间: 2017/11/13 15:18:08
 * 描述:
 */

namespace BLL.learn
{
    public class learnErrorBackBLL
    {
        /// <summary>
        /// 保存错题反馈
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static void save(Models.learnErrorBack m)
        {
            using (DAL.learn.learnErrorBackDAL dal = new DAL.learn.learnErrorBackDAL())
            {
                dal.save(m);
            }
        }
    }
}
