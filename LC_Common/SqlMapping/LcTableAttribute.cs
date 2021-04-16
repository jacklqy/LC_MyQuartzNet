using System;
using System.Collections.Generic;
using System.Text;

namespace LC_Common.SqlMapping
{
    /// <summary>
    /// 做表名称的别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)] //约束只能修饰类
    public class LcTableAttribute : LcAbstractMappingAttribute
    {
        //private string _Name = null;
        public LcTableAttribute(string tableName) : base(tableName)
        {
            //this._Name = tableName;
        }
        //public string GetName()
        //{
        //    return this._Name;
        //}
    }
}
