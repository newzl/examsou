/*
 * 创建人:   hllive
 * 创建时间: 2017/11/8 17:18:49
 * 描述:
 */

using Newtonsoft.Json;
namespace Models
{
    public class learn
    {
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class learnInfo
    {
        public int lid { get; set; }
        public int sid { get; set; }
        public string stype { get; set; }
        [JsonIgnore]
        public string sname { get; set; }
        public int chs { get; set; }
        public int mus { get; set; }
        public int jus { get; set; }
        public int fis { get; set; }
        public int qas { get; set; }
        public int lss { get; set; }
        public int mcs { get; set; }
        public int? rows { get; set; }
    }
}
