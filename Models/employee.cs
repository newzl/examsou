using Newtonsoft.Json;
using System;

namespace Models
{
    //后台session存储
    public class employeeSession
    {
        //public int id { get; set; }
        public string eid { get; set; }
    }
    //用于前台cookie存储
    public class employeeCookie
    {
        //public int state { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }
    public class employeeLoginInfo
    {
        //public int id { get; set; }
        public string eid { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }
    //用户详细
    public class employeeDetail
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string photo { get; set; }
        public string nation { get; set; }
        public string degree { get; set; }
        public int state { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string idcard { get; set; }
        public string account { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public string offices { get; set; }
        public string level { get; set; }
        public string jobInfo { get; set; }
        public int logins { get; set; }
        public int learns { get; set; }
        public DateTime lastLoginTime { get; set; }
        public DateTime registerTime { get; set; }
    }
    //级别资料
    [JsonObject(MemberSerialization.OptOut)]
    public class employeePersonal
    {
        [JsonIgnore]
        public string eid { get; set; }
        public string photo { get; set; }
        public string account { get; set; }//不用保存
        public string name { get; set; }
        public int? sex { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int? zzmm { get; set; }
        public int? nation { get; set; }
        public int? degreeid { get; set; }
    }
    public class employeeProfession {
        [JsonIgnore]
        public string eid { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string company { get; set; }
        public int jibie { get; set; }
        public int zhicheng { get; set; }
        public string idcard { get; set; }
        public string bz { get; set; }
    }
    //级别资料 部门科室，级别职称
    [JsonObject(MemberSerialization.OptOut)]
    public class employee_bkjz
    {
        [JsonIgnore]
        public string eid { get; set; }
        public int? bumen { get; set; }
        public int? keshi { get; set; }
        public int? jibie { get; set; }
        public int? zhicheng { get; set; }
    }
}
