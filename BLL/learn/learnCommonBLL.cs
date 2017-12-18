/*
 * 创建人:   hllive
 * 创建时间: 2017/11/8 17:19:37
 * 描述:
 */

namespace BLL.learn
{
    public class learnCommonBLL
    {
        /// <summary>
        /// 获得学习基本信息
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="sid"></param>
        /// <param name="stype"></param>
        /// <returns></returns>
        public static Models.learnInfo getLearnBaseInfo(int lid, int sid, string stype)
        {
            using (DAL.learn.learnCommonDAL dal = new DAL.learn.learnCommonDAL())
            {
                return dal.getLearnBaseInfo(lid, sid, stype);
            }
        }
    }
}
