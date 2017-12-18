using Models;

namespace BLL.root
{
    public class subksBLL
    {
        public static int save(subks m, string style)
        {
            using (DAL.root.subksDAL dal = new DAL.root.subksDAL())
            {
                if (m.id > 0)
                {
                    return dal.update(m, getTable(style));
                }
                else
                {
                    return dal.save(m, style);
                }
            }
        }
        public static void inState(int id, bool state, string style)
        {
            using (DAL.root.subksDAL dal = new DAL.root.subksDAL())
            {
                dal.inState(id, state, getTable(style));
            }
        }
        public static subks getEntity(int id, string style)
        {
            using (DAL.root.subksDAL dal = new DAL.root.subksDAL())
            {
                return dal.getEntity(id, getTable(style));
            }
        }
        private static string getTable(string style)
        {
            string tb = null;
            switch (style)
            {
                case "qa": tb = "QASubject"; break;
                case "fi": tb = "FillVacancySubject"; break;
                case "ls": tb = "LSSubject"; break;
                case "mc": tb = "MCJXSubject"; break;
            }
            return tb;
        }
    }
}
