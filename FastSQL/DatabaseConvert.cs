using System;
using System.Data;

namespace FastSQL
{
    public class DatabaseConvert
    {
        public static SqlDbType? ConvertToDbType(Type type)
        {
            if (type == typeof(int) || type == typeof(int?))
            {
                return SqlDbType.Int;
            }
            if (type == typeof(long) || type == typeof(long?))
            {
                return SqlDbType.BigInt;
            }
            if (type == typeof(bool) || type == typeof(bool?))
            {
                return SqlDbType.Bit;
            }
            if (type == typeof(bool) || type == typeof(bool?))
            {
                return SqlDbType.Bit;
            }
            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return SqlDbType.DateTime;
            }
            if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return SqlDbType.Decimal;
            }
            if (type == typeof(float) || type == typeof(float?))
            {
                return SqlDbType.Float;
            }
            if (type == typeof(byte[]))
            {
                return SqlDbType.VarBinary;
            }
            if (type == typeof(string))
            {
                return SqlDbType.NVarChar;
            }
            if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return SqlDbType.UniqueIdentifier;
            }
            return null;
        }
    }
}