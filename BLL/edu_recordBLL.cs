namespace BLL
{
    public class edu_recordBLL
    {
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
