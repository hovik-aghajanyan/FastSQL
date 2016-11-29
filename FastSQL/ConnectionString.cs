using System;
using System.Runtime.InteropServices;
using System.Security;

namespace FastSQL
{
    public class ConnectionString
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public bool IsIntegratedSecurity { get; set; }

        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public string GetConnectionString()
        {
            if (IsIntegratedSecurity)
                return $@"Data Source={Server};Initial Catalog={DatabaseName};Integrated Security=true;User ID={UserName};Password={SecureStringToString(Password)};";
            return $@"user id={UserName};password={SecureStringToString(Password)};server={Server};database={DatabaseName};";
        }
    }
}