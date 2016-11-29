using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FastSQL.Attributes;

namespace FastSQL.BaseModel
{
    public abstract class ParameterModelBase
    {
        public List<SqlParameter> GetParameters()
        {
            List<SqlParameter> res = new List<SqlParameter>();
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                var name = $"@{propertyInfo.Name}";
                var type = DatabaseConvert.ConvertToDbType(propertyInfo.PropertyType);
                var value = propertyInfo.GetValue(this);
                var paramAttr = (propertyInfo.GetCustomAttributes(typeof(ParameterAttribute), true).FirstOrDefault() as ParameterAttribute);
                if (paramAttr != null)
                {
                    if (paramAttr.Name != null)
                    {
                        name = paramAttr.Name;
                    }
                    if (paramAttr.Type != null)
                    {
                        type = paramAttr.Type;
                    }
                    if (paramAttr.DefaultValue != null && value == null)
                    {
                        value = paramAttr.DefaultValue;
                    }
                }
                res.Add(type == null ? new SqlParameter(name, type) {Value = value} : new SqlParameter(name, value));
            }
            return res;
        }

       
    }
    
}