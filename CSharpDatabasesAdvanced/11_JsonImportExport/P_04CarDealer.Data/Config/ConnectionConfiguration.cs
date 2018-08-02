using System;
using System.Collections.Generic;
using System.Text;

namespace P_04CarDealer.Data.Config
{
  public   class ConnectionConfiguration
    {
        public static string ConnectionString => @"Server=(localdb)\MSSQLLocalDB;Database=CarDealer;Integrated Security=True;";
    }
}
