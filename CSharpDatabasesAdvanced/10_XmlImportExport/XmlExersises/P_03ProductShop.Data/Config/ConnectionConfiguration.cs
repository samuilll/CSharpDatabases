using System;
using System.Collections.Generic;
using System.Text;

namespace P_03ProductShop.Data.Config
{
  public   class ConnectionConfiguration
    {
        public static string ConnectionString => @"Server=(localdb)\MSSQLLocalDB;Database=ProductShop;Integrated Security=True;";
    }
}
