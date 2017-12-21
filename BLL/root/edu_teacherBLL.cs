using Models;


namespace BLL.root
{
    public class edu_teacherBLL
    {
        /// <summary>
        /// 添加课件     12
        /// </summary>
        /// <param name="objkeJian"></param>
        /// <returns></returns>
        public int save(edu_teacher objedu_teacher)
        {
            using (DAL.root.edu_teacherDAL dal = new DAL.root.edu_teacherDAL())
            {
                if (objedu_teacher.id != 0)
                    return dal.UpdateKeJian(objedu_teacher);
                else
                    return dal.AddEdu_teacher(objedu_teacher);
            }
        }
        public edu_teacher getTeacher(int id)
        {
            using (DAL.root.edu_teacherDAL dal = new DAL.root.edu_teacherDAL())
            {
                return dal.getTeacher(id);
            }
        }
        public static string getTeacher()
        {
            using (DAL.root.edu_teacherDAL dal = new DAL.root.edu_teacherDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getTeacher());
                }
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTeacher(int id)
        {
            using (DAL.root.edu_teacherDAL dal = new DAL.root.edu_teacherDAL())
            {
                return dal.DelTeacher(id);
            }
        }
    }
}
