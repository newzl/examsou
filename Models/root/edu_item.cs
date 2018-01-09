using System;
using System.Collections.Generic;

namespace Models
{
    public class edu_item
    {
        public int id { get; set; }
        public int pid { get; set; }
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
        public int xsNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime cutOffDate { get; set; }
        public string scidArr { get; set; }
    }
    //查看继教项目详细信息
    public class edu_item_detail
    {
        public string pic { get; set; }
        public string name { get; set; }
        public string typ { get; set; }
        public decimal xf { get; set; }
        public string bh { get; set; }
        public string fzdw { get; set; }
        public string detail { get; set; }
        public List<edu_item_detail_keJian> keJian { get; set; }

    }
    public class edu_item_detail_keJian
    {
        public string title { get; set; }
        public string typ { get; set; }
        public string teacher { get; set; }
        public decimal xs { get; set; }//学时
        public int sc { get; set; }//时长
    }
}
