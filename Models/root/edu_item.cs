using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class edu_item
    {
        public int id { get; set; }
        public string bh { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public int typ { get; set; }
        public decimal xf { get; set; }
        public string fzr { get; set; }
        public string fzdw { get; set; }
        public int teacher { get; set; }
        public string detail { get; set; }
        public int scid { get; set; }
        public DateTime runDate { get; set; }
        public int learns { get; set; }
        public bool isHome { get; set; }
        public DateTime createTime { get; set; }
        public bool valid { get; set; }
        public bool isDel { get; set; }
    }
}
