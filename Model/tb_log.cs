using LC_Common.SqlFilter;
using LC_Common.SqlMapping;
using System;

namespace Model
{
    [LcTable("tb_log")]
    public class tb_log : BaseModel
    {
        [LcKey]
        public long id { get; set; }
        ///// <summary>
        ///// 数据库字段是mqpathid  但是程序是mqpath_id   
        ///// </summary>
        //[LcCloumn("mqpathid")]
        public int mqpathid { get; set; }

        public string mqpath { get; set; }

        public string methodname { get; set; }

        public string info { get; set; }

        public DateTime createtime { get; set; }

        public DateTime? updatetime { get; set; }
    }
}
