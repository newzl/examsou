using DAL;
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
            using (edu_myitemDAL dal=new edu_myitemDAL())
            {
                dal.save(eid,itid);
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
        public void beginLearn(int miid, string eid) {
            using (edu_myitemDAL dal = new edu_myitemDAL())
            {
                dal.beginLearn(miid,eid);
            }
        }
    }
}
