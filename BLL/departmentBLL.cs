/*
 * 创建人:   hllive
 * 创建时间: 2017/10/20 10:17:02
 * 描述:
 */

using Models;
namespace BLL
{
    public class departmentBLL
    {
        /// <summary>
        /// 获得部门和科室select
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string getSelect(int companyID, int pid)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getSelect(companyID, pid));
                }
            }
        }
        /// <summary>
        /// 保存部门或科室
        /// </summary>
        /// <param name="m"></param>
        public static void save(department m)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                if (m.id > 0)
                {
                    dal.updateks(m);//修改科室
                }
                else
                {
                    dal.save(m);//添加科室
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        public static void delete(int ID)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                dal.delete(ID);
            }
        }
        public static void deletebm(int ID)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                dal.deletebm(ID);
            }
        }
        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static department getEntity(int id)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                return dal.getEntity(id);
            }
        }

        //组卷获得部门室  用于组卷 获得参考对象
        public static string get_zj_BM(int companyID)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.get_zj_BM(companyID));
                }
            }
        }
        //组卷获得部科室  用于组卷 获得参考对象
        public static string get_zj_KS(int companyID, int pid)
        {
            using (DAL.departmentDAL dal = new DAL.departmentDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.get_zj_KS(companyID, pid));
                }
            }
        }
    }
}
