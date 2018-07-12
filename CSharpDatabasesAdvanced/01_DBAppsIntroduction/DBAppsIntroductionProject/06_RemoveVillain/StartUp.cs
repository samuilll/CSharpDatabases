using System;
using System.Data.SqlClient;

namespace _06_RemoveVillain
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

                Console.WriteLine("Please eneter villain Id:");
                int villainId = int.Parse(Console.ReadLine());
                string villainName = GetVillainName(villainId, connection);

                if (villainName==null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }

                else
                {
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        int releasedMinions = ReleaseMinions(connection, villainId, transaction);
                        int rowsChanged = DeleteVillain(villainId, connection, transaction);

                        transaction.Commit();

                        Console.WriteLine($"{villainName} was deleted.");
                        Console.WriteLine($"{releasedMinions} minions were released.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        Console.WriteLine("There has been an error in transaction");
                        return;
                    }
                }
            


            }
        }

        private static int DeleteVillain(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string commandString = "delete from Villains where Id=@Id";

            using (var command = new SqlCommand(commandString, connection,transaction))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                int result = command.ExecuteNonQuery();

                return result;
            }
        }

        private static int ReleaseMinions(SqlConnection connection, int villainId, SqlTransaction transaction)
        {
            string commandString = "delete from MinionsVillains where VillainId=@Id";

            using (var command = new SqlCommand(commandString, connection,transaction))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                int result = command.ExecuteNonQuery();

                return result;
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string commandString = "select name from Villains where id=@Id";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                string result = (string)command.ExecuteScalar();

                return result;
            }
        }
    }
}
