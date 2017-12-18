
namespace Models
{
    public class subject
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string sname { get; set; }
        public int levelID { get; set; }
        public string introduce { get; set; }
        public bool valid { get; set; }
    }
    //题库详情
    public class subjectDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string introduce { get; set; }
        public string level { get; set; }
        public int total { get; set; }
        public int learns { get; set; }
        public int comments { get; set; }
        public int evaluate { get; set; }
        public bool isLearn { get; set; }
    }
}
