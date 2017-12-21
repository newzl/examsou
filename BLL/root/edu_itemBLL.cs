using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BLL.root
{
    public class edu_itemBLL
    {
        /// <summary>
        /// 根据id获取项目对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public edu_item ByIdEduItem(int id)
        {
            using (DAL.root.edu_itemDAL dal = new DAL.root.edu_itemDAL())
            {
                return dal.ByIdEduItem(id);
            }
        }
        public static string GetTyp()
        {
            using (DAL.root.edu_itemDAL dal = new DAL.root.edu_itemDAL())
            {
                using (Common.JsonParse js = new Common.JsonParse())
                {
                    return js.convert(dal.GetTyp());
                }
            }
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="objEdu_item"></param>
        /// <returns></returns>
        public int save(edu_item objEdu_item)
        {
            using (DAL.root.edu_itemDAL dal = new DAL.root.edu_itemDAL())
            {
                if (objEdu_item.id > 0)
                    return dal.UpdateEduItem(objEdu_item);
                else
                    return dal.AddEduItem(objEdu_item);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelEduItem(int id)
        {
            using (DAL.root.edu_itemDAL dal = new DAL.root.edu_itemDAL())
            {
                return dal.DelEduItem(id);
            }
        }
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="objEdu_item"></param>
        ///// <returns></returns>
        //public int UpdateEduItem(edu_item objEdu_item)
        //{
        //    using (DAL.root.edu_itemDAL dal = new DAL.root.edu_itemDAL())
        //    {
        //        return dal.UpdateEduItem(objEdu_item);
        //    }
        //}
    }
}
