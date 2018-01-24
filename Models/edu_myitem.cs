using System.Collections.Generic;

namespace Models
{
    public class edu_myitem
    {
    }
    public class edu_index
    {
        public int miid { get; set; }
        public int inkjid { get; set; }//正在学习课件ID，等于0表示未学习记录（前台默认为课件第一个）
        public string name { get; set; }
        //public int scid { get; set; }
        public decimal xxjd { get; set; }
        public decimal bxk { get; set; }
        public decimal xxk { get; set; }
        public List<edu_index_kjList> list { get; set; }
    }
    public class edu_index_kjList
    {
        public int id { get; set; }
        public string title { get; set; }
        public string typ { get; set; }
        public decimal xs { get; set; }
        public int sc { get; set; }
        public decimal perc { get; set; }
    }
    public class myItemPost//用于API控制器POST数据
    {
        public int miid { get; set; }
    }
    //正在学习的项目
    public class edu_myitem_inlearn {
        public int miid { get; set; }//我的继教项目ID
        public int itid { get; set; }//项目ID
    }
}
