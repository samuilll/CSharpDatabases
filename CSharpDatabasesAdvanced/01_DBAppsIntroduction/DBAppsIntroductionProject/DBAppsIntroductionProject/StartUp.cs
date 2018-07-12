using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBAppsIntroductionProject
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //var connection = new SqlConnection(
            //    @"Server=(localdb)\MSSQLLocalDB;" +
            //    "Database=SoftUni;" +
            //    "Integrated Security=True;"
            //    );

            //connection.Open();

            //Console.WriteLine("Enter the town you want to insert:");
            //var town = Console.ReadLine();
            //using (connection)
            //{
            //    var transaction = connection.BeginTransaction();
            //    var command = new SqlCommand($"delete from Towns where [Name]= '{town}'",connection,transaction);
            //   var totalSalary =  command.ExecuteNonQuery();

            //    // transaction.Rollback();
            //    transaction.Commit();

            //    Console.WriteLine(totalSalary.GetType());
            //    Console.WriteLine(totalSalary.ToString());
            //}

            //    Console.WriteLine("EmployeeCount");
            //     -------------------------------------------------------------------------------------------------
            var connection = new SqlConnection(
                            @"Server=(localdb)\MSSQLLocalDB;" +
                            "Database=SoftUni;" +
                            "Integrated Security=True;"
                            );

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand("select firstname, lastname,salary, jobTitle from Employees",connection);

                var reader = command.ExecuteReader();

                var people = new List<Employee>();

                while (reader.Read())
                {
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var jobTitle = (string)reader["JobTitle"];
                    var salary = (decimal)reader["Salary"];

                    var currentPerson = new Employee(firstName,lastName,salary,jobTitle);

                    people.Add(currentPerson);
                }

                var orderByJobTitle = people.GroupBy(p => p.JobTitle).OrderByDescending(g => g.Count()).ToList();

                foreach (var group in orderByJobTitle)
                {
                    Console.WriteLine($"{group.Key} -> {group.Count()} People :");

                    var counter = 1;

                    foreach (var employee in group)
                    {
                        Console.WriteLine($"{counter}. {employee}");
                        counter++;
                    }
                }
            }


            
        }
    }
}
