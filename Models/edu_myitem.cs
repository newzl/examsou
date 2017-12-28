using System.Collections.Generic;

namespace Models
{
    public class edu_myitem
    {
    }
    public class edu_index
    {
        public int miid { get; set; }
        public int inkjid { get; set; }
        public string name { get; set; }
        public int scid { get; set; }
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
        public int total { get; set; }
        public int minut { get; set; }
        public decimal perc { get; set; }
    }
    public class myItemPost
    {
        public int miid { get; set; }
    }
}
