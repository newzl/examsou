using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Text;

namespace Common
{
    public class JsonParse : IDisposable
    {
        #region 释放资源
        bool dis;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (dis) return;
            if (disposing)
                dis = true;
        }
        ~JsonParse() { Dispose(false); }
        #endregion
        /// <summary>
        /// DataTable转换为json不处理时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string convert(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0) return JsonConvert.SerializeObject(dt, new DataTableConverter());
            else return null;
        }
        /// <summary>
        /// DataTable转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToJson(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0) return JsonConvert.SerializeObject(dt, new DataTableConverter(), new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });
            else return null;
        }
        /// <summary>
        /// DataTable转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format">时间格式：默认：“yyyy-MM-dd HH:mm”</param>
        /// <returns></returns>
        public string ToJson(DataTable dt, string format)
        {
            format = string.IsNullOrEmpty(format) ? "yyyy-MM-dd HH:mm" : format;
            if (dt != null && dt.Rows.Count > 0) return JsonConvert.SerializeObject(dt, new DataTableConverter(), new IsoDateTimeConverter { DateTimeFormat = format });
            else return null;
        }
        /// <summary>
        /// 表格形式转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string ToTablePage(DataTable dt, int count)
        {
            if (count > 0 && dt.Rows.Count > 0) return "{\"count\":" + count + ",\"data\":" + this.ToJson(dt) + "}";
            else return "{\"count\":0,\"data\":{}}"; ;
        }
        /// <summary>
        /// 表格形式转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="count"></param>
        /// <param name="format">时间格式：默认：“yyyy-MM-dd HH:mm”</param>
        /// <returns></returns>
        public string ToTablePage(DataTable dt, int count, string format)
        {
            if (count > 0 && dt.Rows.Count > 0) return "{\"count\":" + count + ",\"data\":" + this.ToJson(dt, format) + "}";
            else return "{\"count\":0,\"data\":{}}"; ;
        }
        /// <summary>
        /// layui框架table模块数据转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string ToLayuiTable(DataTable dt, int count)
        {
            if (count > 0 && dt.Rows.Count > 0) return "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + this.ToJson(dt) + "}";
            else return "{\"code\":1,\"msg\":\"没有符合条件的记录\",\"count\":0,\"data\":[]}";
        }
        /// <summary>
        /// layui框架table模块数据转换为json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="count"></param>
        /// <param name="format">时间格式：默认：“yyyy-MM-dd HH:mm”</param>
        /// <returns></returns>
        public string ToLayuiTable(DataTable dt, int count, string format)
        {
            if (count > 0 && dt.Rows.Count > 0) return "{\"code\":0,\"msg\":\"\",\"count\":" + count + ",\"data\":" + this.ToJson(dt, format) + "}";
            else return "{\"code\":1,\"msg\":\"没有符合条件的记录\",\"count\":0,\"data\":[]}";
        }
        /// <summary>
        /// 获得带有children的json字符串,字段里必须有（id，pid）两个字段
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="id">父级id</param>
        /// <returns></returns>
        public string ToChildren(DataTable dt, int id)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select("pid=" + id);
                if (rows.Length == 0) return string.Empty;
                StringBuilder str = new StringBuilder();
                foreach (DataRow row in rows)
                {
                    str.Append("{");
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        if (row.Table.Columns[i].ColumnName != "pid")
                        {
                            if (i != 0) str.Append(",");
                            str.Append("\"");
                            str.Append(row.Table.Columns[i].ColumnName);
                            str.Append("\":\"");
                            str.Append(row[i]);
                            str.Append("\"");
                        }
                    }
                    if (ToChildren(dt, (int)row["id"]).Length > 0)
                    {
                        str.Append(",\"children\":");
                        str.Append(ToChildren(dt, (int)row["id"]));
                        str.Append("},");
                    }
                    else
                    {
                        str.Append("},");
                    }
                }
                string json = str[str.Length - 1] == ',' ? str.ToString(0, str.Length - 1) : str.ToString();
                return "[" + json + "]";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从一个对象信息生成Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 从Json字符串生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ToObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
