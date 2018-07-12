using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_InreaseMinionAge
{
    class Program
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
                Console.WriteLine("Please Insert Minions' Ids:");
                var Ids = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                UpdateMinionsTable(connection, Ids);

                PrintAllMinions(connection);
            }
        }

        private static void PrintAllMinions(SqlConnection connection)
        {
            string commandString = "select name,age from Minions";

            var minions = new List<string>();

            using (var command = new SqlCommand(commandString, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minions.Add($"{(string)reader["Name"]} {(int)reader["Age"]}");
                    }
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine, minions));
        }

        private static void UpdateMinionsTable(SqlConnection connection, System.Collections.Generic.List<int> Ids)
        {
            foreach (var id in Ids)
            {
                string commandString = "update minions" +
               " set Age+=1, " +
               "Name = CONCAT(UPPER(LEFT(Name,1)),Lower(Right(Name,Len(Name)-1))) " +
               "where Id =@id";

                using (var command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
