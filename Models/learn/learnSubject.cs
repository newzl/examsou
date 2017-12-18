/*
 * 创建人:   hllive
 * 创建时间: 2017/11/7 16:41:05
 * 描述:
 */
using Newtonsoft.Json;
namespace Models
{
    public class learnSubject
    {
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class learnIndex {
        [JsonIgnore]
        public int lid { get; set; }
        [JsonIgnore]
        public int sid { get; set; }
        //[JsonIgnore]
        public string sname { get; set; }
        public int counts { get; set; }
        public int total { get; set; }
        public int yes { get; set; }
        public int wrong { get; set; }
        public int today { get; set; }
        public decimal xxjd { get; set; }
        public decimal zwd { get; set; }
        public decimal zql { get; set; }
        public decimal jrxx { get; set; }
        public decimal dds { get; set; }
        public decimal dcs { get; set; }
    }
    public class inlearn {
        public int lid { get; set; }
        public int sid { get; set; }
        public string sname { get; set; }
    }
}
