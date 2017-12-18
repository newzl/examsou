/*
 * 创建人:   hllive
 * 创建时间: 2017/11/7 10:56:57
 * 描述:收藏试题
 */

namespace Models
{
    public class learnCollect
    {
        public int cid { get; set; }//收藏ID 如果等于0则保存，否则删除
        public int lid { get; set; }//我的题库ID
        public int sid { get; set; }//题库类型ID
        public string stype { get; set; }//题型
        public int kid { get; set; }//题目ID
    }
}
