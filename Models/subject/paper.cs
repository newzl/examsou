/*
 * 创建人:   hllive
 * 创建时间: 2017/12/8 11:38:07
 * 描述:
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
    public class paper
    {
        public string name { get; set; }
        public int useTime { get; set; }
        public List<dan> dan { get; set; }
        public List<duo> duo { get; set; }
        public List<pan> pan { get; set; }
    }
}
