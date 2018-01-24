using System.Data;
using Models;

namespace BLL.exam
{
    /// <summary>
    /// 模拟考试业务逻辑处理类
    /// </summary>
    public class simulateBLL
    {
        /// <summary>
        /// 通过项目ID获得模拟考试试卷
        /// </summary>
        /// <param name="itid"></param>
        /// <returns></returns>
        public simulatePaper getSimulatePaper(int itid)
        {
            using (DAL.exam.simulateDAL dal = new DAL.exam.simulateDAL())
            {
                DataSet ds = dal.getSimulatePaper(itid);
                if (ds != null)
                {
                    using (Common.DataParse dp = new Common.DataParse())
                    {
                        simulatePaper m = new simulatePaper
                        {
                            dan = dp.DataTableToList<danans>(ds.Tables[0]),
                            duo = dp.DataTableToList<duoans>(ds.Tables[1]),
                            pan = dp.DataTableToList<panans>(ds.Tables[2])
                        };
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
        /// 查看模拟考试试卷
        /// </summary>
        /// <param name="esid"></param>
        /// <returns></returns>
        public showSimulate showSimulatePaper(int esid)
        {
            using (DAL.exam.simulateDAL dal = new DAL.exam.simulateDAL())
            {
                DataSet ds = dal.showSimulatePaper(esid);
                if (ds != null)
                {
                    using (Common.DataParse dp = new Common.DataParse())
                    {
                        showSimulate m = ds != null ? dp.DataRowToEntity<showSimulate>(ds.Tables[0].Rows[0]) : null;
                        m.dan = dp.DataTableToList<danans>(ds.Tables[1]);
                        m.duo = dp.DataTableToList<duoans>(ds.Tables[2]);
                        m.pan = dp.DataTableToList<panans>(ds.Tables[3]);
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
        /// 保存模拟考试
        /// </summary>
        /// <param name="m"></param>
        public void save(simulate m)
        {
            using (DAL.exam.simulateDAL dal = new DAL.exam.simulateDAL())
            {
                dal.save(m);
            }
        }
        public void delete(int id)
        {
            using (DAL.exam.simulateDAL dal = new DAL.exam.simulateDAL())
            {
                dal.delete(id);
            }
        }
    }
}