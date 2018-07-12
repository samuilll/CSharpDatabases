using System;
using System.Data.SqlClient;

namespace _03_MinionNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; " +
                                                           "Database=MinionsDB;" +
                                                          "Integrated Security=True;");

            connection.Open();

            using (connection)
            {
                int villainId = int.Parse(Console.ReadLine());

                string villainName = GetVillainName(connection, villainId);

                if (villainName != null)
                {
                    Console.WriteLine($"Villain: {villainName}");
                }
                else
                {
                    Console.WriteLine("No villain with ID {villainId} exists in the database.");
                    return;
                }

                PrintNames(villainId, connection);
            }
        }

        private static void PrintNames(int villainId, SqlConnection connection)
        {
            var getVillainsString = "select m.Name,m.Age" +
                                    " from Minions m" +
                                    " JOIN MinionsVillains mv ON mv.MinionId = m.Id" +
                                    " where mv.VillainId = @Id " +
                                    "ORDER BY m.Name";
            using (var command = new SqlCommand(getVillainsString, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                        return;
                    }

                    int counter = 1;
                    while (reader.Read())
                    {
                        Console.WriteLine($"{counter}. {reader["Name"]} {reader["Age"]}");
                        counter++;
                    }
                }
            }
        }

        private static string GetVillainName(SqlConnection connection, int villainId)
        {
            string isThereVillainString = $"select Name from Villains where Id=@Id";

            var isThereVillainCommand = new SqlCommand(isThereVillainString, connection);

            isThereVillainCommand.Parameters.AddWithValue("@Id", villainId);

            string name = (string)isThereVillainCommand.ExecuteScalar();

            return name;
        }
    }
}
