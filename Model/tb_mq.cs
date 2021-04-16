using LC_Common.SqlFilter;
using LC_Common.SqlMapping;
using System;

namespace Model
{
    [LcTable("tb_mq")]
    public class tb_mq : BaseModel
    {
        [LcKey]
        public long id { get; set; }

        public string mqname { get; set; }

        public DateTime createtime { get; set; }
    }
}
