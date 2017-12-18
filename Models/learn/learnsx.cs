/*
 * 创建人:   hllive
 * 创建时间: 2017/11/10 14:38:27
 * 描述:
 */

namespace Models
{
    public class learnsx
    {
        public int lid { get; set; }
        public int sid { get; set; }
        public string stype { get; set; }
        public int fx { get; set; }
        public int row { get; set; }
    }
    public class learnks {
        public int sid { get; set; }
        public string stype { get; set; }
        public int page { get; set; }
    }
    public class saveLearn {
        public int lid { get; set; }
        public int sid { get; set; }
        public string stype { get; set; }
        public int row { get; set; }
        public int kid { get; set; }
        public string answer { get; set; }
        public bool result { get; set; }
    }
}
