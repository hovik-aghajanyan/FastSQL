using System;
using System.Data;

namespace FastSQL.Attributes
{
    public class ParameterAttribute:Attribute
    {
        public string Name { get; set; } = null;
        public SqlDbType? Type { get; set; } = null;
        public object DefaultValue { get; set; } = null;
    }
}