using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05_ChangeTownNameCasing
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
                Console.WriteLine("Please enter the name of the country you want to change:");
                string countryName = Console.ReadLine();

                if (!ThereIsSuchACountry(countryName, connection))
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                int CountryId = GetCountryId(countryName, connection);

                int countOfTownsInTheCountry = CountOfTownsToChange(CountryId, countryName, connection);

                if (countOfTownsInTheCountry==0)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                ChangeTheTowns(CountryId, connection);

                string townsSequence = GetTheTowns(CountryId, connection);

                Console.WriteLine($"{countOfTownsInTheCountry} town names were affected. ");

                Console.WriteLine(townsSequence);

            }
        }

        private static string GetTheTowns(int countryId, SqlConnection connection)
        {
            string commandString = ("select name from towns where CountryCode = @Id");

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", countryId);

                using (var reader = command.ExecuteReader())
                {
                    var towns = new List<string>();

                    while (reader.Read())
                    {
                        towns.Add((string)reader["Name"]);
                    }

                    return $"[{string.Join(", ", towns)}]";
                }
            }
        }

        private static void ChangeTheTowns(int countryId, SqlConnection connection)
        {
            string commandString = ("Update Towns Set Name = Upper(Name) Where CountryCode = @Id");

            using (var command = new SqlCommand(commandString,connection))
            {
                command.Parameters.AddWithValue("@Id", countryId);
                command.ExecuteNonQuery();
            }
        }

        private static  int CountOfTownsToChange(int Id,string countryName, SqlConnection connection)
        {

            string commandString = "select count(*) from Towns where CountryCode=@Id";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);

                int result = (int)command.ExecuteScalar();

                return result;
            }
        }

        private static  int GetCountryId(string countryName, SqlConnection connection)
        {
            string commandString = "select Id from countries where Name = @countryName";

            using (var command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                int Id = (int)command.ExecuteScalar();

                return Id;
            }

        }

        private static  bool ThereIsSuchACountry(string countryName, SqlConnection connection)
        {
            string commandString = "select name from Countries where name=@name";

            using (var command = new SqlCommand(commandString,connection))
            {
                command.Parameters.AddWithValue("@name", countryName);

                string result = (string)command.ExecuteScalar();

                return result != null;
            }
        }
    }
}
