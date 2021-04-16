using LC_Common.SqlFilter;
using LC_Common.SqlMapping;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal
{
    /// <summary>
    /// 用来完成sql语句的缓存
    /// 每张表都是几个固定的sql
    /// 泛型缓存
    /// </summary>
    public class SqlBuilder<T> where T : BaseModel
    {
        private static string _FindSql = null;
        private static string _InsertSql = null;
        private static string _UpdateSql = null;
        private static string _DeleteSql = null;
        static SqlBuilder()
        {
            Type type = typeof(T);
            {
                string columnsString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
                _FindSql = $"select {columnsString} from [{type.GetMappingName()}] where id=";
            }
            {
                string columnsString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
                string valuesString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
                _InsertSql = $"insert into [{type.GetMappingName()}] ({columnsString}) values({valuesString}) SELECT @@Identity;";//不能直接拼装--sql注入问题
            }
            {
                string setString = string.Join(",", type.GetPropertiesWithoutKey().Select(p => $"{p.GetMappingName()}=@{p.GetMappingName()}"));
                _UpdateSql = $"update [{type.GetMappingName()}] set {setString} where id=@id;";//不能直接拼装--sql注入问题
            }
            {
                _DeleteSql = $"delete from [{type.GetMappingName()}] where id=@id";
            }
        }

        /// <summary>
        /// 以Id=结尾，可以直接添加参数
        /// </summary>
        /// <returns></returns>
        public static string GetFindSql()
        {
            return _FindSql;
        }

        public static string GetInsertSql()
        {
            return _InsertSql;
        }
        public static string GetUpdateSql()
        {
            return _UpdateSql;
        }
        public static string GetDeleteSql()
        {
            return _DeleteSql;
        }
    }

}
