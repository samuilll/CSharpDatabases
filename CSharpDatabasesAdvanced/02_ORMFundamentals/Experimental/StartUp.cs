using Experimental.Data;
using Experimental.Data.Entities;
using System;
using System.Linq;

namespace Experimental
{
  public  class StartUp
    {
        static void Main(string[] args)
        {

            string connectioString = "Server=(localdb)\\MSSQLLocalDB;" +
                                     " Database = MiniORM;" +
                                     " Integrated Security = True;";

            var context = new SoftUniContext(connectioString);

            var employee = new Employee()
            {
                FirstName = "Strahil",
                LastName = "Gorilov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            };

             var modEmployee = context.Employees.Last();
             employee.FirstName = "Modified";

             context.Employees.Add(employee);

            context.SaveChanges();

            Console.WriteLine("Hey");
        }
    }
}
