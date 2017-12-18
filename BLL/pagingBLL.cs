/*
 * 创建人:   hllive
 * 创建时间: 2017/10/20 15:00:40
 * 描述:
 */
using System.Data;

namespace BLL
{
    public class pagingBLL
    {
        #region 返回layui格式的json
        public static string runLaypage(Models.paging pag)
        {
            using (DAL.pagingDAL dal = new DAL.pagingDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    int count = 0;
                    DataTable dt = dal.execPaging(pag, out count);
                    return jp.ToLayuiTable(dt, count);
                }
            }
        }
        public static string runLaypage(Models.paging pag, string format)
        {
            using (DAL.pagingDAL dal = new DAL.pagingDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    int count = 0;
                    DataTable dt = dal.execPaging(pag, out count);
                    return jp.ToLayuiTable(dt, count, format);
                }
            }
        }
        #endregion

        #region 返回table格式的josn
        public static string runpage(Models.paging pag)
        {
            using (DAL.pagingDAL dal = new DAL.pagingDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    int count = 0;
                    DataTable dt = dal.execPaging(pag, out count);
                    return jp.ToTablePage(dt, count);
                }
            }
        }
        public static string runpage(Models.paging pag, string format)
        {
            using (DAL.pagingDAL dal = new DAL.pagingDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    int count = 0;
                    DataTable dt = dal.execPaging(pag, out count);
                    return jp.ToTablePage(dt, count, format);
                }
            }
        }
        #endregion

    }
}
