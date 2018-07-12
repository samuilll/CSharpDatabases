using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07_PrintAllMinionNames
{
    class StartUp
    {
        static void Main(string[] args)
        {

            var connection = new SqlConnection(
                                   @"Server=(localdb)\MSSQLLocalDB;" +
                                   "Database=MinionsDB;" +
                                   "Integrated Security=True;"
                                   );

            connection.Open();

            using (connection)
            {
                string commandString = "select name from Minions";

                var minions = new List<string>();

                using (var command = new SqlCommand(commandString,connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            minions.Add((string)reader["Name"]);
                        }
                    }
                }

                for (int i = 0; i < minions.Count/2; i++)
                {
                    Console.WriteLine(minions[i]);
                    Console.WriteLine(minions[minions.Count-1-i]);
                }
                if (minions.Count%2!=0)
                {
                    Console.WriteLine(minions[minions.Count/2]);
                }
            }
        }
    }
}
