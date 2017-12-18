using Newtonsoft.Json;
namespace Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class department
    {
        public int id { get; set; }
        [JsonIgnore]
        public int companyID { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
        [JsonIgnore]
        public int type { get; set; }
        public int must { get; set; }
        public bool valid { get; set; }
        public bool isExam { get; set; }
    }
}
