using System;
using System.Data.SqlClient;

namespace _02_VillainNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; " +
                                               "Database=MinionsDB;" +
                                              "Integrated Security=True;");

            connection.Open();


            var villainsInfo = "select v.Name as Name,e.Count as Count from Villains v" +
                     " JOIN (select VillainId, COUNT(1) as Count from MinionsVillains mv" +
                     " group by VillainId) as e ON v.Id = e.VillainId Where e.Count>=3" +
                     " order by e.Count desc";

            using (connection)
            {
                var villainsWithMinionsCommand = new SqlCommand(villainsInfo, connection);

                using (villainsWithMinionsCommand)
                {
                    var reader = villainsWithMinionsCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Count"]}");
                    }
                }
            }
        }
    }
}
