using System.Collections.Generic;

namespace Models
{
    public class home
    {
        public home_employee employee { get; set; }
        public List<home_item> item { get; set; }
        public List<home_teacher> teacher { get; set; }
    }
    public class home_employee {
        public string photo { get; set; }
        public string name { get; set; }
        public int? sex { get; set; }
        public string company { get; set; }
        public int logins { get; set; }
        public string time { get; set; }
    }
    public class home_item {
        public int id { get; set; }
        public string  pic { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public decimal xf { get; set; }
        public int learns { get; set; }
        public int bxxs { get; set; }//必修学时
        public string qzrq { get; set; }//起止日期
    }
    public class home_teacher {
        public int id { get; set; }
        public string pic { get; set; }
        public string name { get; set; }
        public string  zc { get; set; }//职称
        public string detail { get; set; }
        public int kj { get; set; }//课件数量
    }
}
