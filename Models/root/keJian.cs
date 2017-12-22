using System;

namespace Models
{
    /// <summary>
    /// 课件实体类
    /// </summary>
    public class keJian
    {
        public int id { get; set; }

        public int teacher { get; set; }

        public int itid { get; set; }

        public string title { get; set; }

        public string cont { get; set; }

        public int cont_typ { get; set; }

        public int typ { get; set; }

        public decimal xueshi { get; set; }

        public int xueshi_minute { get; set; }

        public DateTime createTime { get; set; }

        public string curl { get; set; }
        public string author { get; set; }
    }
}