using Models;

namespace BLL.root
{
    public class submuBLL
    {
        public static int save(submu m)
        {
            using (DAL.root.submuDAL dal = new DAL.root.submuDAL())
            {
                if (m.id > 0)
                {
                    return dal.update(m);
                }
                else
                {
                    return dal.save(m);
                }
            }
        }
        public static void inState(int id, bool state)
        {
            using (DAL.root.submuDAL dal = new DAL.root.submuDAL())
            {
                dal.inState(id, state);
            }
        }
        public static submu getEntity(int id)
        {
            using (DAL.root.submuDAL dal = new DAL.root.submuDAL())
            {
                return dal.getEntity(id);
            }
        }
    }
}
