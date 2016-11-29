using System;
using System.Data;
using System.Linq;
using FastSQL.Attributes;

namespace FastSQL.BaseModel
{
    public class TableModelBase
    {
        public virtual void Parse(DataRow row, bool ignorePrefix = false)
        {
            String prefix = (this.GetType().GetCustomAttributes(typeof(TableModelAttribute), false).FirstOrDefault() as TableModelAttribute)?.ColumnsPrefix;
            if (!ignorePrefix && prefix != null)
            {
                foreach (var propertyInfo in this.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(this,row[prefix + propertyInfo.Name] != DBNull.Value ? row[prefix + propertyInfo.Name] : null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                foreach (var propertyInfo in this.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(this,
                            row[propertyInfo.Name] != DBNull.Value ? row[propertyInfo.Name] : null);
                    }
                    catch (Exception)
                    {
                        //ignored
                    }
                }
            }
        }

        public virtual void Parse(DataRow row, string prefix)
        {
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                try
                {
                    propertyInfo.SetValue(this, row[prefix + propertyInfo.Name] != DBNull.Value ? row[prefix + propertyInfo.Name] : null);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}