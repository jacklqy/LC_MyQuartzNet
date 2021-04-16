using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LC_Common.SqlMapping
{
    public static class MappingExtend
    {
        /// <summary>
        /// 获取映射名称
        /// </summary>
        /// <param name="type">可以是type 也可以是Property</param>
        /// <returns></returns>
        public static string GetMappingName(this MemberInfo type)
        {
            if (type.IsDefined(typeof(LcAbstractMappingAttribute), true))
            {
                LcAbstractMappingAttribute lcTableAttribute = type.GetCustomAttribute<LcAbstractMappingAttribute>();
                return lcTableAttribute.GetName();
            }
            else
            {
                return type.Name;
            }
        }

        //public static string GetTableName(this Type type)
        //{
        //    if (type.IsDefined(typeof(LcTableAttribute), true))
        //    {
        //        LcTableAttribute lcTableAttribute = type.GetCustomAttribute<LcTableAttribute>();
        //        return lcTableAttribute.GetName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}

        //public static string GetCloumnName(this PropertyInfo prop)
        //{
        //    if (prop.IsDefined(typeof(LcCloumnAttribute), true))
        //    {
        //        LcCloumnAttribute lcTableAttribute = prop.GetCustomAttribute<LcCloumnAttribute>();
        //        return lcTableAttribute.GetName();
        //    }
        //    else
        //    {
        //        return prop.Name;
        //    }
        //}
    }
}
