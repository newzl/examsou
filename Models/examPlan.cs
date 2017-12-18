/*
 * 创建人:   hllive
 * 创建时间: 2017/12/7 15:08:22
 * 描述:
 */
using System;
using System.Collections.Generic;

namespace Models
{
    public class examPlanBase
    {
        public string name { set; get; }
        public string explain { set; get; }
        public decimal passScore { set; get; }
        public int examNum { set; get; }
        public int useTime { set; get; }
        public DateTime? startTime { set; get; }
        public DateTime? endTime { set; get; }
        public bool isShow { set; get; }
        public bool isCopy { set; get; }
        public bool state { set; get; }
        public int chs { set; get; }
        public decimal chvs { set; get; }
        public int mus { set; get; }
        public decimal muvs { set; get; }
        public int jus { set; get; }
        public decimal juvs { set; get; }
    }
    //保存组卷
    public class examPlan : examPlanBase
    {
        public int id { set; get; }
        public int companyID { set; get; }
        public int type { set; get; }
        public string sidList { set; get; }
        public bool isLimit { set; get; }
        public string examJoin { set; get; }
        public int creator { set; get; }
        public string chlist { set; get; }
        public string mulist { set; get; }
        public string julist { set; get; }
    }
    //查看手工组卷entity
    public class showsgzj : examPlanBase
    {
        public int chss { set; get; }
        public int muss { set; get; }
        public int juss { set; get; }
        public decimal scores { set; get; }
        public List<examJoin> examJoin { set; get; }
    }
    //查看随机组卷entity
    public class showsjzj : examPlanBase
    {
        public int chss { set; get; }
        public int muss { set; get; }
        public int juss { set; get; }
        public decimal scores { set; get; }
        public List<zjsubclss> zjsubclss { set; get; }
        public List<examJoin> examJoin { set; get; }
    }
    public class examJoin
    {
        public string bm { get; set; }
        public string ks { get; set; }
        public string jb { get; set; }
        public string zc { get; set; }
    }
    public class zjsubclss {
        public string level { get; set; }
        public string name { get; set; }
    }
}
