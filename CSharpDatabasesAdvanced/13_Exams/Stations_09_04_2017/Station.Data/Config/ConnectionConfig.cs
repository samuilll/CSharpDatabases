using System;
using System.Collections.Generic;
using System.Text;

namespace Travelling.Data.Config
{
   public class ConnectionConfig
    {
        public static string ConnectionString => @"Server=(localdb)\MSSQLLocalDB;Database=Travelling;Integrated Security=True;";
    }
}
