using System;

namespace FastSQL.Exceptions
{
    public class ConnectionAlreadyAssingedException:Exception
    {
        public ConnectionAlreadyAssingedException():base("Before change connection set default connection to null")
        {
            
        }
    }
}