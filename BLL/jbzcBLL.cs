/*
 * 创建人:   hllive
 * 创建时间: 2017/10/20 11:56:45
 * 描述:
 */

namespace BLL
{
    public class jbzcBLL
    {
        public static string getSelect(int companyID, int pid)
        {
            using (DAL.jbzcDAL dal = new DAL.jbzcDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    if (pid == 0)
                    {
                        return jp.convert(dal.getjb());
                    }
                    else
                    {
                        return jp.convert(dal.getzc(companyID, pid));
                    }
                }
            }
        }
        /// <summary>
        /// 获得级别（包括公共题库）
        /// </summary>
        /// <returns></returns>
        public static string getAllJb()
        {
            using (DAL.jbzcDAL dal = new DAL.jbzcDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getAllJb());
                }
            }
        }
        /// <summary>
        /// 保存职称
        /// </summary>
        /// <param name="m"></param>
        public static void save(Models.jobinfo m)
        {
            using (DAL.jbzcDAL dal = new DAL.jbzcDAL())
            {
                if (m.id > 0)
                {
                    dal.updatezc(m);//修改
                }
                else
                {
                    dal.savezc(m);//添加
                }
            }
        }
        /// <summary>
        /// 删除职称
        /// </summary>
        /// <param name="id"></param>
        public static void deletezc(int id)
        {
            using (DAL.jbzcDAL dal = new DAL.jbzcDAL())
            {
                dal.deletezc(id);
            }
        }
        /// <summary>
        /// 获得试题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Models.jobinfo getEntity(int id)
        {
            using (DAL.jbzcDAL dal = new DAL.jbzcDAL())
            {
                return dal.getEntity(id);
            }
        }
    }
}
