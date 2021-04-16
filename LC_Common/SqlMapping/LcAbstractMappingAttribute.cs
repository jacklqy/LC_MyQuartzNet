using System;
using System.Collections.Generic;
using System.Text;

namespace LC_Common.SqlMapping
{
    public abstract class LcAbstractMappingAttribute : Attribute
    {
        private string _Name = null;
        public LcAbstractMappingAttribute(string name)
        {
            this._Name = name;
        }
        public string GetName()
        {
            return this._Name;
        }
    }
}
