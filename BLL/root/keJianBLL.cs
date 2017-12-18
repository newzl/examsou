using Models;


namespace BLL.root
{
    public class keJianBLL
    {
        /// <summary>
        /// 添加课件
        /// </summary>
        /// <param name="objkeJian"></param>
        /// <returns></returns>
        public int save(keJian objkeJian)
        {
            using (DAL.root.keJianDAL dal = new DAL.root.keJianDAL())
            {
                if (objkeJian.id != 0)
                    return dal.UpdateKeJian(objkeJian);
                else
                    return dal.AddKeJian(objkeJian);
            }
        }
        public keJian getEntity(int id)
        {
            using (DAL.root.keJianDAL dal = new DAL.root.keJianDAL())
            {
                return dal.getEntity(id);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Del(int id)
        {
            using (DAL.root.keJianDAL dal = new DAL.root.keJianDAL())
            {
                return dal.Del(id);
            }
        }
    }
}
