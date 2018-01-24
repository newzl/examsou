
namespace Models
{
    public class readyExamJson
    {
        public int code { get; set; }
        public readyExam data { get; set; }
    }
    //准备考试
    public class readyExam
    {
        public int itid { get; set; }
        public int miid { get; set; }
        public string name { get; set; }
        public decimal scores { get; set; }
        public decimal passScore { get; set; }
        public int useTime { get; set; }
        public string qzrq { get; set; }//起止日期
        public bool allow { get; set; }//是否允许考试
    }
}
