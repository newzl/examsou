/*
 * 创建人:   hllive
 * 创建时间: 2017/10/18 9:50:40
 * 描述:
 */
using System.Data.SqlClient;

namespace DAL.account
{
    public class validateEmailDAL
    {
        //保存动态码
        public bool saveValidateEmail(Models.validateEmail entity)
        {
            SqlParameter[] pars = 
            {
                new SqlParameter("@account",entity.account),
                new SqlParameter("@token",entity.token),
                new SqlParameter("@sendID",entity.sendID),
                new SqlParameter("@vtype",entity.vtype)
            };
            return SqlHelper.RunProcedures("[acc_saveValidateEmail]", pars) > 0;
        }
    }
}
