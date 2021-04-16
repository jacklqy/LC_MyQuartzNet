using LC_Common.SqlFilter;
using LC_Common.SqlMapping;
using System;

namespace Model
{

    [LcTable("tb_error")]
    public class tb_error : BaseModel
    {
        [LcKey]
        public long id { get; set; }

        public int mqpathid { get; set; }

        public string mqpath { get; set; }

        public string methodname { get; set; }

        public string info { get; set; }

        public DateTime createtime { get; set; }

    }
}
