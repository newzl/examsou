/*
 * 创建人:   hllive
 * 创建时间: 2017/12/8 11:34:45
 * 描述:判断题
 */

namespace Models
{
    public class pan
    {
        public int id { get; set; }
        public string sub { get; set; }
    }
    public class panans : pan
    {
        public string ans { get; set; }
    }
}
