using System;

namespace FastSQL.Attributes
{
    public class TableModelAttribute:Attribute
    {
        public string TableName { get; set; }
        public string ColumnsPrefix { get; set; }
    }
}