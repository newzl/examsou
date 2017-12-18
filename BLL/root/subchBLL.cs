using Models;
namespace BLL.root
{
    public class subchBLL
    {
        public static int save(subch m)
        {
            using (DAL.root.subchDAL dal = new DAL.root.subchDAL())
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
            using (DAL.root.subchDAL dal = new DAL.root.subchDAL())
            {
                dal.inState(id, state);
            }
        }
        public static subch getEntity(int id)
        {
            using (DAL.root.subchDAL dal = new DAL.root.subchDAL())
            {
                return dal.getEntity(id);
            }
        }
    }
}
