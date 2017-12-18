using Models;
namespace BLL.root
{
    public class subjuBLL
    {
        public static int save(subju m)
        {
            using (DAL.root.subjuDAL dal = new DAL.root.subjuDAL())
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
            using (DAL.root.subjuDAL dal = new DAL.root.subjuDAL())
            {
                dal.inState(id, state);
            }
        }
        public static subju getEntity(int id)
        {
            using (DAL.root.subjuDAL dal = new DAL.root.subjuDAL())
            {
                return dal.getEntity(id);
            }
        }
    }
}
