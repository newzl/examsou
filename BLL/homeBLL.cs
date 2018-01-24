using System.Data;
using Models;

namespace BLL
{
    public class homeBLL
    {
        public home execHome(string eid)
        {
            using (DAL.homeDAL dal = new DAL.homeDAL())
            {
                DataSet ds = dal.execHome(eid);
                using (Common.DataParse dp = new Common.DataParse())
                {
                    home m = new home
                    {
                        employee = ds.Tables[0].Rows.Count > 0 ? dp.DataRowToEntity<home_employee>(ds.Tables[0].Rows[0]) : null,
                        item = dp.DataTableToList<home_item>(ds.Tables[1]),
                        teacher = dp.DataTableToList<home_teacher>(ds.Tables[2])
                    };
                    ds.Clear();//清除数据
                    return m;
                }
            }
        }
    }
}