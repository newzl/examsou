
namespace Models
{
    //分页实体
    public class paging
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// where条件
        /// </summary>
        public string where { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int pageNo { get; set; }
    }
}
