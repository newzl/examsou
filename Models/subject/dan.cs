/*
 * 创建人:   hllive
 * 创建时间: 2017/12/8 11:30:02
 * 描述:单选题实体
 */

namespace Models
{
    public class dan
    {
        public int id { get; set; }
        public string sub { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
    }
    public class danans : dan
    {
        public string ans { get; set; }
    }
}
