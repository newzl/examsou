/*
 * 创建人:   hllive
 * 创建时间: 2017/11/24 11:29:31
 * 描述:
 */

namespace BLL.root
{
    public class toolBLL
    {
        public static int save(Models.replacegif m)
        {
            using (DAL.root.toolDAL dal = new DAL.root.toolDAL())
            {
                return dal.save(m);
            }
        }
        public static void delete(int id)
        {
            using (DAL.root.toolDAL dal = new DAL.root.toolDAL())
            {
                dal.delete(id);
            }
        }
        public static string getData()
        {
            using (DAL.root.toolDAL dal = new DAL.root.toolDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getData());
                }
            }
        }
    }
}
