using Newtonsoft.Json;
namespace Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class subbase
    {
        public int id { get; set; }
        public int sid { get; set; }
        public string snav { get; set; }
        public string title { get; set; }
        public string subs { get; set; }//题目
        public string explain { get; set; }//解析
        public bool instate { get; set; }//是否入库
        [JsonIgnore]
        public int creator { get; set; }
    }
    public class subch : subbase
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string result { get; set; }
    }
    public class submu : subch
    {
        public string F { get; set; }
    }
    public class subju : subbase
    {
        public bool result { get; set; }
    }
    public class subks : subbase
    {
        public string result { get; set; }
    }
}
