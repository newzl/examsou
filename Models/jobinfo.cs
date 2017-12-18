/*
 * 创建人:   hllive
 * 创建时间: 2017/10/23 10:58:58
 * 描述: 职称试题
 */
using Newtonsoft.Json;
namespace Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class jobinfo
    {
        public int id { get; set; }
        [JsonIgnore]
        public int companyID { get; set; }
        public int jbid { get; set; }
        public string name { get; set; }
        public bool valid { get; set; }
    }
}
