using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LC_Common.SqlFilter
{
    public static class FilterExtend
    {
        /// <summary>
        /// 过滤自增主键，返回全部属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutKey(this Type type)
        {
            return type.GetProperties().Where(p => !p.IsDefined(typeof(LcKeyAttribute), true));
        }
    }
}
