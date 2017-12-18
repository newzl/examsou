/*
 * 创建人:   hllive
 * 创建时间: 2017/11/13 15:24:24
 * 描述:
 */

namespace BLL.learn
{
    public class learnNotesBLL
    {
        /// <summary>
        /// 保存笔记
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static void save(Models.learnNotes m)
        {
            using (DAL.learn.learnNotesDAL dal = new DAL.learn.learnNotesDAL())
            {
                dal.save(m);
            }
        }
    }
}
