
namespace BLL
{
    public class subjectClassBLL
    {
        /// <summary>
        /// 保存题库类型
        /// </summary>
        /// <param name="m"></param>
        public static void save(Models.subject m)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                if (m.id > 0)
                {
                    dal.update(m);
                }
                else
                {
                    dal.save(m);
                }
            }
        }
        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Models.subject getEntity(int id)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                return dal.getEntity(id);
            }
        }
        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="valid"></param>
        /// <param name="id"></param>
        public static void setValid(bool valid, int id)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                dal.setValid(valid, id);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public static void delete(int id)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                dal.delete(id);
            }
        }
        /// <summary>
        /// 通过id获得name
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string getName(int sid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                return dal.getName(sid);
            }
        }
        /// <summary>
        /// 选择题库数据
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>childrenJSON</returns>
        public static string seleSubject(string eid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToChildren(dal.seleSubject(eid), 2);
                }
            }
        }
        /// <summary>
        /// 题库详情获得实体
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Models.subjectDetail detailEntity(int sid, string eid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                return dal.detailEntity(sid, eid);
            }
        }
        /// <summary>
        /// 查看科目系列
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string subjectTree(int sid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.ToChildren(dal.subjectTree(sid), sid);
                }
            }
        }
        /// <summary>
        /// 获得树数据
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string getTree(int pid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getTree(pid));
                }
            }
        }
        /// <summary>
        /// 获得树数据 -用于题库录入
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string getSelTree(int pid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getSelTree(pid));
                }
            }
        }

        /// <summary>
        /// 获得树数据-维护
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string getManTree(int pid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.getManTree(pid));
                }
            }
        }
        /// <summary>
        /// 值获得select
        /// pid<0：通过leve获得
        /// level<0：通过pid获得
        /// </summary>
        /// <param name="level"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string sele(int level, int pid)
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    if (level >= 0 && pid < 0)
                    {
                        return jp.convert(dal.sel_level(level));
                    }
                    else if (pid >= 0 && level < 0)
                    {
                        return jp.convert(dal.sel_pid(pid));
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public static string Get_scid()
        {
            using (DAL.subjectClassDAL dal = new DAL.subjectClassDAL())
            {
                using (Common.JsonParse jp = new Common.JsonParse())
                {
                    return jp.convert(dal.Get_scid());
                }
            }
        }
    }
}