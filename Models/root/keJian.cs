using System;

namespace Models
{
    /// <summary>
    /// 课件实体类
    /// </summary>
    public class keJian
    {
        public int id { get; set; }
        /// <summary>
        /// 跟subjectLevel表中的ID关联
        /// </summary>
        public int levelID { get; set; }
        /// <summary>
        /// 跟subjectClass 表中的ID值关联
        /// </summary>
        public int sid { get; set; }
        /// <summary>
        /// 课件标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 课件内容（地址）
        /// </summary>
        public string cont { get; set; }
        /// <summary>
        /// 内容类型（sys_code 表 typ=1）
        /// </summary>
        public int cont_typ { get; set; }
        /// <summary>
        /// 课程类型（sys_code 表 typ=2）
        /// </summary>
        public int typ { get; set; }
        /// <summary>
        /// 学时  
        /// </summary>
        public double xueshi { get; set; }
        /// <summary>
        /// 学时时长
        /// </summary>
        public int xueshi_minute { get; set; }
        /// <summary>
        /// 添加课件时间（默认getdate()）
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 扩展属性用来获取修改时内容地址，当修改成功后被删除
        /// </summary>
        public string curl { get; set; }
    }
}