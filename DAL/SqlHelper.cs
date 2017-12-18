using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DAL
{
    public class SqlHelper
    {
        //连接字符串
        private static readonly string connString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;

        #region 执行语句
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdStr">sql语句</param>
        /// <param name="parames">参数</param>
        /// <returns></returns>
        public static int ExcuteNonQuery(string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parames != null)
                    {
                        cmd.Parameters.AddRange(parames);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>【不常用】
        /// 返回首行首列的值
        /// </summary>
        /// <param name="cmdStr">sql语句</param>
        /// <param name="parames">参数</param>
        /// <returns></returns>
        public static int ExcuteScalre(string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    conn.Open();
                    if (parames != null) cmd.Parameters.AddRange(parames);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        public static SqlDataReader ExcuteSqlDataReader(string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    conn.Open();
                    if (parames != null) cmd.Parameters.AddRange(parames);
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection); ;
                }
            }
        }
        /// <summary>
        /// 返回DataSet类型的数据
        /// </summary>
        public static DataSet ExcuteDataSet(string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        if (parames != null) cmd.Parameters.AddRange(parames);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "DS");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }
        /// <summary>
        /// 返回DataTable类型的数据
        /// </summary>
        public static DataTable ExcuteDataTable(string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        if (parames != null) cmd.Parameters.AddRange(parames);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cmd.Parameters.Clear();
                        return dt;
                    }
                }
            }
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parames">存储过程参数</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string procName, IDataParameter[] parames, string TName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    DataSet ds = new DataSet();
                    conn.Open();
                    ada.SelectCommand = BuildQueryCommand(conn, procName, parames);
                    ada.Fill(ds, TName);
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parames">存储过程参数</param>
        /// <returns>DataTable</returns>
        public static DataTable RunProcedure(string procName, IDataParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    ada.SelectCommand = BuildQueryCommand(conn, procName, parames);
                    ada.Fill(dt);
                    return dt;
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parames">存储过程参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int RunProcedures(string procName, IDataParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = BuildQueryCommand(conn, procName, parames);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 构建SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="parames">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection conn, string procName, IDataParameter[] parames)
        {
            using (SqlCommand command = new SqlCommand(procName, conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parames)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
                return command;
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string cmdStr, params SqlParameter[] parames)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, cmdStr, parames));
            if (cmdresult == 0)
                return false;
            else
                return true;
        }
        
        //返回一个对象
        public static T ExecuteEntity<T>(string cmdText, params SqlParameter[] parames)
        {
            T obj = default(T);
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    if (parames != null) cmd.Parameters.AddRange(parames);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            obj = ExecuteDataReader<T>(dr);
                            break;
                        }
                    }
                }
            }
            return obj;
        }
        //返回一个对象 存储过程
        public static T RunProcedureEntity<T>(string procName, params SqlParameter[] parames)
        {
            T obj = default(T);
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parames != null) cmd.Parameters.AddRange(parames);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            obj = ExecuteDataReader<T>(dr);
                            break;
                        }
                    }
                }
            }
            return obj;
        }
        private static T ExecuteDataReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            int columnCount = dr.FieldCount;
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                for (int i = 0; i < columnCount; i++)
                {
                    string columnName = dr.GetName(i);
                    if (string.Compare(propertyName, columnName, true) == 0)
                    {
                        object value = dr.GetValue(i);
                        if (value != null && value != DBNull.Value)
                        {
                            propertyInfo.SetValue(obj, value, null);
                        }
                        break;
                    }
                }
            }
            return obj;
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdStr, params SqlParameter[] parames)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    if (parames != null) cmd.Parameters.AddRange(parames);
                    conn.Open();
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return obj;
                }
            }
        }
        #endregion
    }
}
