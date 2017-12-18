/*
 * 创建人:   hllive
 * 创建时间: 2017/10/25 9:52:22
 * 描述:
 */

namespace BLL
{
    public class publicBLL
    {
        /// <summary>
        /// 获得c_bm表里数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getcbm(int type)
        {
            using (DAL.publicDAL dal = new DAL.publicDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getcbm(type));
                }
            }
        }
        /// <summary>
        /// 获得区域表 select
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string getxzbm(string pxzbm)
        {
            using (DAL.publicDAL dal = new DAL.publicDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getxzbm(pxzbm));
                }
            }
        }

    }
}
