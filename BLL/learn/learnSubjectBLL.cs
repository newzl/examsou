
namespace BLL
{
    public class learnSubjectBLL
    {
        /// <summary>
        ///  保存学习题库,保存成功  题库学习人数加一
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static int saveLearnSubject(string eid, int sid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                return dal.saveLearnSubject(eid, sid);
            }
        }
        /// <summary>
        /// 取消学习
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="eid"></param>
        public static void cancel(int lid, string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                dal.cancelLearnSubject(lid, eid, "cancel");
            }
        }
        /// <summary>
        /// 清空学习
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="eid"></param>
        public static void empty(int lid, string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                dal.cancelLearnSubject(lid, eid, "empty");
            }
        }
        /// <summary>
        /// 开始学习
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="eid"></param>
        public static void beginLearn(int lid, string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                dal.beginLearn(lid, eid);
            }
        }
        /// <summary>
        /// 我的学习首页
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static Models.learnIndex learnIndex(string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                return dal.learnIndex(eid);
            }
        }
        /// <summary>
        /// 通过eid获得正在学习的题库
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static Models.inlearn getInlearn(string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                return dal.getInlearn(eid);
            }
        }
        /// <summary>
        /// 章节练习获得数据
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="scid"></param>
        /// <returns></returns>
        public string getChapter(string eid, int scid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getChapter(eid, scid));
                }
            }
        }
        /// <summary>
        /// 我的题库list，通过eid获得我的题库table
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static string getMyLearnTab(string eid)
        {
            using (DAL.learnSubjectDAl dal = new DAL.learnSubjectDAl())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getMyLearnTab(eid));
                }
            }
        }
    }
}
