using System.Data;

namespace BLL.exam
{
    public class formalBLL
    {
        // 准备考试
        public Models.readyExamJson readyExam(string eid)
        {
            using (DAL.exam.formalDAL dal = new DAL.exam.formalDAL())
            {
                int _code = 0;
                DataTable dt = dal.readyExam(eid, ref _code);
                using (Common.DataParse dp = new Common.DataParse())
                {
                    return new Models.readyExamJson
                    {
                        code = _code,//1:获取数据成功；0:项目已过期
                        data = dt != null ? dp.DataRowToEntity<Models.readyExam>(dt.Rows[0]) : null
                    };
                }
            }
        }
        /// <summary>
        /// 通过项目ID获得正规考试试卷
        /// </summary>
        /// <param name="itid"></param>
        /// <returns></returns>
        public Models.formalPaper getFormalPaper(int itid)
        {
            using (DAL.exam.formalDAL dal = new DAL.exam.formalDAL())
            {
                int _code = 0;
                DataSet ds = dal.getFormalPaper(itid, ref _code);
                if (ds != null)
                {
                    using (Common.DataParse dp = new Common.DataParse())
                    {
                        Models.formalPaper m = ds != null ? dp.DataRowToEntity<Models.formalPaper>(ds.Tables[0].Rows[0]) : null;
                        m.code = _code;
                        m.itid = itid;
                        m.dan = dp.DataTableToList<Models.dan>(ds.Tables[1]);
                        m.duo = dp.DataTableToList<Models.duo>(ds.Tables[2]);
                        m.pan = dp.DataTableToList<Models.pan>(ds.Tables[3]);
                        ds.Clear();//清除数据
                        return m;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 保存正规考试试卷
        /// </summary>
        /// <param name="m"></param>
        public void save(Models.formalExam m)
        {
            using (DAL.exam.formalDAL dal = new DAL.exam.formalDAL())
            {
                dal.save(m);
            }
        }
        /// <summary>
        /// 查看正规考试试卷
        /// </summary>
        /// <param name="ansid"></param>
        /// <returns></returns>
        public Models.showFormal showFormalPaper(int ansid)
        {
            using (DAL.exam.formalDAL dal = new DAL.exam.formalDAL())
            {
                DataSet ds = dal.showFormal(ansid);
                if (ds != null)
                {
                    using (Common.DataParse dp = new Common.DataParse())
                    {
                        Models.showFormal m = ds != null ? dp.DataRowToEntity<Models.showFormal>(ds.Tables[0].Rows[0]) : null;
                        m.dan = dp.DataTableToList<Models.danans>(ds.Tables[1]);
                        m.duo = dp.DataTableToList<Models.duoans>(ds.Tables[2]);
                        m.pan = dp.DataTableToList<Models.panans>(ds.Tables[3]);
                        ds.Clear();//清除数据
                        return m;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}