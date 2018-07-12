using System;
using System.Data.SqlClient;
using System.Linq;

namespace _04_AddMinion
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; " +
                                               "Database=MinionsDB;" +
                                               "Integrated Security=True;");

            connection.Open();

            Console.Write("Minion: ");

            string[] minionParams = Console.ReadLine()
                               .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                               .ToArray();

            string minionName = minionParams[0];
            int minionAge = int.Parse(minionParams[1]);
            string minionTown = minionParams[2];

            Console.Write("Villain: ");
            string villainName = Console.ReadLine().Trim();

            using (connection)
            {
                CheckForTheTownAndInsertIfNecessery(connection, minionTown);

                CheckForTheVillainAndInsertIfNecessery(connection, villainName);

                InsertMinnionToMinnions(connection, minionName, minionAge, minionTown);

                InsertMinionAndVillainInRelatingTable(connection, minionName, villainName);
            }
        }

        private static void InsertMinionAndVillainInRelatingTable(SqlConnection connection, string minionName, string villainName)
        {
            int villainId = GetVillainId(villainName, connection);

            int minionId = GetMinnionId(minionName, connection);

            string commandString = $"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES ({minionId},{villainId})";

            var command = new SqlCommand(commandString, connection);

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");
        }

        private static int GetMinnionId(object minionName, SqlConnection connection)
        {
            string commandString = "select id from minions where name=@Name";
            var command = new SqlCommand    (commandString, connection);
            command.Parameters.AddWithValue("@Name",minionName);

            return (int)command.ExecuteScalar();
        }

        private static int GetVillainId(string villainName, SqlConnection connection)
        {
            string commandString = "select id from villains where name=@Name";
            var command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@Name", villainName);

            return (int)command.ExecuteScalar();
        }

        private static void InsertMinnionToMinnions(SqlConnection connection, string minionName, int minionAge, string minionTown)
        {
            string commandString = $"INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),(@Name, @Age, @TownId)";

            int townId = ((int)(new SqlCommand($"select id from towns where name = '{minionTown}'", connection).ExecuteScalar()));

            var insertMinionCommand = new SqlCommand(commandString, connection);

            insertMinionCommand.Parameters.AddWithValue("@Name", minionName);
            insertMinionCommand.Parameters.AddWithValue("@Age", minionAge);
            insertMinionCommand.Parameters.AddWithValue("@TownId", townId);

            insertMinionCommand.ExecuteNonQuery();
        }

        private static void CheckForTheVillainAndInsertIfNecessery(SqlConnection connection, string villainName)
        {
            string commandAsString = "select name from Villains where Name=@Name";

            var command = new SqlCommand(commandAsString, connection);

            command.Parameters.AddWithValue("@Name", villainName);

            InsertVillain(connection, villainName, command);
        }

        private static void InsertVillain(SqlConnection connection, string villainName, SqlCommand command)
        {
            if (command.ExecuteScalar() == null)
            {
                var insertCommandString = "INSERT INTO Villains (Name, EvilnessFactorId) VALUES (@Name,4)";

                var insertCommand = new SqlCommand(insertCommandString, connection);

                insertCommand.Parameters.AddWithValue("@Name", villainName);

                insertCommand.ExecuteNonQuery();

                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
        }

        private static void CheckForTheTownAndInsertIfNecessery(SqlConnection connection, string minionTown)
        {
            string commandAsString = "select name from Towns where Name=@Name";

            var command = new SqlCommand(commandAsString, connection);

            command.Parameters.AddWithValue("@Name", minionTown);

            var check = command.ExecuteScalar();

            if (check == null)
            {
                InsertTown(connection, minionTown);
            }
        }

        private static void InsertTown(SqlConnection connection, string minionTown)
        {
            var insertCommandString = " INSERT INTO Towns([Name], CountryCode) VALUES(@Name, 1)";

            var insertCommand = new SqlCommand(insertCommandString, connection);

            insertCommand.Parameters.AddWithValue("@Name", minionTown);

            insertCommand.ExecuteNonQuery();

            Console.WriteLine($"Town {minionTown} was added to the database.");
        }
    }
}
