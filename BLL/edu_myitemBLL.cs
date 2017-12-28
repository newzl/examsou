using DAL;
using System.Data;
namespace BLL
{
    public class edu_myitemBLL
    {
        /// <summary>
        /// 保存学习（立即学习）
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="itid"></param>
        public void save(string eid, int itid)
        {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                dal.save(eid, itid);
            }
        }
        /// <summary>
        /// 删除学习
        /// </summary>
        /// <param name="id"></param>
        public void delete(int id)
        {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                dal.delete(id);
            }
        }
        public void beginLearn(int miid, string eid)
        {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                dal.beginLearn(miid, eid);
            }
        }
        /// <summary>
        /// 我的学习页获得数据
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public Models.edu_index edu_index(string eid)
        {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                DataSet ds = dal.edu_index(eid);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    using (Common.DataParse dp = new Common.DataParse())
                    {
                        Models.edu_index m = dp.DataRowToEntity<Models.edu_index>(ds.Tables[0].Rows[0]);
                        m.list = dp.DataTableToList<Models.edu_index_kjList>(ds.Tables[1]);
                        return m;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 学习记录list
        /// </summary>/myitem/record
        /// <param name="eid"></param>
        /// <returns></returns>
        public string learnRecord(string eid)
        {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.learnRecord(eid), null);
                }
            }
        }
    }
}
