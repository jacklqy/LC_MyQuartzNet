using System;
using System.Collections.Generic;
using System.Text;

namespace LC_Common.SqlMapping
{
    /// <summary>
    /// 做字段名称的别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)] //约束只能修属性
    public class LcCloumnAttribute : LcAbstractMappingAttribute
    {
        //private string _Name = null;
        public LcCloumnAttribute(string cloumnName) : base(cloumnName)
        {
            //this._Name = cloumnName;
        }
        //public string GetName()
        //{
        //    return this._Name;
        //}
    }
}
