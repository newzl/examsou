/*
 * 创建人:   hllive
 * 创建时间: 2017/10/25 14:49:23
 * 描述:
 */

namespace BLL
{
    public class companyBLL
    {
        /// <summary>
        /// 通过xzbm获得单位select
        /// </summary>
        /// <param name="xzbm"></param>
        /// <returns></returns>
        public static string getcompany(string xzbm)
        {
            using (DAL.companyDAL dal = new DAL.companyDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getcompany(xzbm));
                }
            }
        }
    }
}
