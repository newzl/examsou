
namespace BLL.exam
{
    public class examTestBLL
    {
        public static string getExamTest(int sid)
        {
            using (DAL.exam.examTestDAL dal = new DAL.exam.examTestDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToJson(dal.getExamTest(sid));
                }
            }
        }
    }
}
