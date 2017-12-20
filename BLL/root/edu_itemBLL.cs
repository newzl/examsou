using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.root
{
    public class edu_itemBLL
    {
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
    }
}
