namespace BLL
{
    public class edu_recordBLL
    {

        /// <summary>
        /// 获得正在学习课件list
        /// </summary>
        /// <param name="miid"></param>
        /// <returns></returns>
        public string getCourse(int miid)
        {
            using (DAL.edu_recordDAL dal = new DAL.edu_recordDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getCourse(miid));
                }
            }
        }
        /// <summary>
        /// 保存学习记录
        /// </summary>
        /// <param name="m"></param>
        public void saveRecord(Models.edu_record m)
        {
            using (DAL.edu_recordDAL dal = new DAL.edu_recordDAL())
            {
                dal.saveRecord(m);
            }
        }
    }
}
