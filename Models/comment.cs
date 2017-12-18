/*
 * 创建人:   hllive
 * 创建时间: 2017/11/1 16:07:16
 * 描述:
 */

namespace Models
{
    public class comment
    {
        public string eid { get; set; }
        public int sid { get; set; }
        public string content { get; set; }
        public int evaluate { get; set; }//评分
    }
}
