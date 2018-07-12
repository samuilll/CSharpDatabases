using System;
using System.Data.SqlClient;

namespace _09_IncreaseSAgeStoredProcedure
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

            Console.WriteLine("Please input minion Id:");

            int id = int.Parse(Console.ReadLine());

            using (connection)
            {

                UpdateMinionAge(connection, id);

                PrintMinion(connection, id);
            }
        }

        private static void PrintMinion(SqlConnection connection, int id)
        {
            string commandString = "select name,age from minions where id=@id";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int age = (int)reader["Age"];

                        Console.WriteLine($"{name} – {age} years old");
                    }
                }
            }
        }

        private static void UpdateMinionAge(SqlConnection connection, int id)
        {
            string commandString = "dbo.usp_GetOlder";

            using (var command = new SqlCommand(commandString, connection) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
