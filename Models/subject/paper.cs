/*
 * 创建人:   hllive
 * 创建时间: 2017/12/8 11:38:07
 * 描述:试卷实体
 */

using System.Collections.Generic;
namespace Models
{
    public class exportPaper//导出试题
    {
        public string name { get; set; }
        public decimal chvs { get; set; }//单选题分数
        public decimal muvs { get; set; }
        public decimal juvs { get; set; }
        public List<dan> dan { get; set; }
        public List<duo> duo { get; set; }
        public List<pan> pan { get; set; }
    }
    /// <summary>
    /// 正规考试试卷
    /// </summary>
    public class formalPaper
    {
        public int code { get; set; }
        public int itid { get; set; }
        public decimal chvs { get; set; }//每题分数
        public decimal chrs { get; set; }//总分
        public decimal muvs { get; set; }
        public decimal murs { get; set; }
        public decimal juvs { get; set; }
        public decimal jurs { get; set; }
        public int useTime { get; set; }//答题时间
        public List<dan> dan { get; set; }
        public List<duo> duo { get; set; }
        public List<pan> pan { get; set; }
    }
    /// <summary>
    /// 查看正规考试试卷
    /// </summary>
    public class showFormal {
        public decimal chvs { get; set; }//每题分数
        public decimal chrs { get; set; }//总分
        public decimal muvs { get; set; }
        public decimal murs { get; set; }
        public decimal juvs { get; set; }
        public decimal jurs { get; set; }
        public string usetime { get; set; }//答题时间
        public string chanswer { get; set; }//用户选择答案
        public string muanswer { get; set; }
        public string juanswer { get; set; }
        public List<danans> dan { get; set; }
        public List<duoans> duo { get; set; }
        public List<panans> pan { get; set; }
    }
    /// <summary>
    /// 模拟考试试卷--带有答案的
    /// </summary>
    public class simulatePaper
    {
        public List<danans> dan { get; set; }
        public List<duoans> duo { get; set; }
        public List<panans> pan { get; set; }
    }
    //查看模拟考试试卷
    public class showSimulate : simulatePaper {
        public string usetime { get; set; }//答题时间
        public string chanswer { get; set; }//用户选择答案
        public string muanswer { get; set; }
        public string juanswer { get; set; }
    }
}
