using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst
{
    public class Solutions
    {
        private SoftUniContext db;

        public void Task16()
        {
           // var eph = db.EmployeesProjects.ToList();

            var employees = db.Employees
                .Where(e => e.EmployeesProjects.Any(ep => ep.Project.Name == "Classic Vest")).ToList();
            // .ToList();

          
            Console.WriteLine();
        }

        public Solutions(SoftUniContext db)
        {
            this.db = db;
        }

        public void Task03()
        {
            var employees = db.Employees
                  .Select(e => new
                  {
                      Id = e.EmployeeId,
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      MiddleName = e.MiddleName,
                      JobTitle = e.JobTitle,
                      Salary = e.Salary
                  })
                  .OrderBy(e => e.Id)
                  .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:f2}");
            }
        }

        public void Task04()
        {
            var employees = db.Employees
                .Where(e => e.Salary > 50000)
               .Select(e => new
               {
                   FirstName = e.FirstName
               })
               .OrderBy(e => e.FirstName)
               .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName}");
            }
        }
        public void Task05()
        {
            var employees = db.Employees
                .Where(e => e.Department.Name == "Research and Development")
                  .Select(e => new
                  {
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      DepartmentName = e.Department.Name,
                      Salary = e.Salary
                  })
                  .OrderBy(e => e.Salary)
                  .ThenByDescending(e => e.FirstName)
                  .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }
        }

        public void Task06()
        {
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = db.Employees.SingleOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            db.SaveChanges();

            var employeesAddresses = db.Employees
                 .Include(e => e.Address)
                 .OrderByDescending(e => e.Address.AddressId)
                 .Take(10)
                 .Select(e => e.Address.AddressText)
                 .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, employeesAddresses));
        }
        public void Task07()
        {

            var employees = db.Employees
                .Where(
                e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)
                )
                .Select
                (e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                    Projects = e.EmployeesProjects.Select(ep => new
                    {
                        Name = ep.Project.Name,
                        StartDate = ep.Project.StartDate,
                        EndDate = ep.Project.EndDate
                    }
                        )
                }
                )
                .Take(30)
                .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.Name} – Manager: {emp.ManagerName}");

                foreach (var project in emp.Projects)
                {
                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", new CultureInfo("EN-gb"));
                    var endDate = project.EndDate != null ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", new CultureInfo("EN-gb")) : "not finished";
                    Console.WriteLine($"--{project.Name} - {startDate} - {endDate}");
                }
            }
        }
        public void Task08()
        {
            var addresses = db.Addresses
                .OrderByDescending(a => a.Employees.Count())
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    AdrText = a.AddressText,
                    TownName = a.Town.Name,
                    EmpCount = a.Employees.Count()
                }
                )
                .ToList();

            foreach (var adr in addresses)
            {
                Console.WriteLine($"{adr.AdrText}, {adr.TownName} - {adr.EmpCount} employees");
            }          
        }
        public void Task09()
        {
            var employee = db.Employees.Find(147);
            var projects = db.EmployeesProjects
                .Where(ep=>ep.EmployeeId==147)
                .Select(ep =>ep.Project.Name)
                .OrderBy(e=>e)
                .ToList();

            Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}{Environment.NewLine}{string.Join(Environment.NewLine,projects)}");  
        }
        public void Task10()
        {
            var employees = db.Employees.Select(e=>e.FirstName).ToList();

            var departments = db.Departments
                 .Where(d => d.Employees.Count >5)
                 .OrderBy(d => d.Employees.Count)
                 .ThenBy(d => d.Name)
                 .Select (d=>
                 
                      $"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName} {Environment.NewLine}" +
                     $"{string.Join(Environment.NewLine,d.Employees.OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName).Select(e=> $"{e.FirstName} {e.LastName} - {e.JobTitle}"))}"
                 
                 )
                 .ToList();


          //  var result = $"{(string.Join($"{Environment.NewLine}{new string('-', 10)}{Environment.NewLine}", departments))}";
             Console.WriteLine(string.Join($"\n{new string('-',10)}\n",departments));          

        }
        public void Task11()
        {
            var projects = db.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate
                })
                .ToList();

            var emptyString = string.Empty;
            foreach (var project in projects.OrderBy(p=>p.Name))
            {
                Console.WriteLine($"{project.Name}{Environment.NewLine}{project.Description}" +
                    $"{Environment.NewLine}{project.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)}");

                emptyString+= $"{project.Name}{Environment.NewLine}{project.Description}" +
                    $"{Environment.NewLine}{project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}";
            }
        }
        public void Task12()
        {
            Func<string,bool> isValid = (name)=> name == "Engineering" ||
                       name == "Tool Design" ||
                       name == "Marketing" ||
                       name == "Information Services";
            var employees = db.Employees
                     .Where(e => isValid(e.Department.Name))
                     .ToList();

            //foreach (var emp in employees)
            //{
            //    emp.Salary += 12 * emp.Salary / 100;
            //}
            //db.SaveChanges();

            foreach (var emp in employees.OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName))
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
            }
                
        }
        public void Task13()
        {
                      db.Employees
                     .Where(e => EF.Functions.Like(e.FirstName,"Sa%"))
                     .Select(e => new
                     {
                         FirstName = e.FirstName,
                         LastName = e.LastName,
                         Job = e.JobTitle,
                         Salary = e.Salary
                     })
                     .OrderBy(e => e.FirstName)
                     .ThenBy(e => e.LastName)
                     .ToList()
                     .ForEach(e => Console.WriteLine($"{e.FirstName} {e.LastName} - {e.Job} - (${e.Salary:f2})"));
    
        }
        public void Task14()
        {
            var projects = db.Projects.ToList();

            var project = projects.Find(p=>p.ProjectId==2);

            projects.Remove(project);

            foreach (var p in projects.Take(10))
            {
                Console.WriteLine(p.Name);
            }

        }
        public void Task15()
        {
            string townToDeleteString = Console.ReadLine();

            var towns = db.Towns.ToList();

            var townToDelete = towns.FirstOrDefault(t => t.Name == townToDeleteString);


            if (townToDelete == null)
            {
                Console.WriteLine("There is no such town");
                return;
            }

            var addresses = db.Addresses.Where(a => a.TownId == townToDelete.TownId).ToList();
            var employees = db.Employees.Where(e => e.Address.Town.TownId==townToDelete.TownId).ToList();

            foreach (var employee in employees)
            {
                employee.Address = null;
            }
            db.Addresses.RemoveRange(addresses);

            db.Towns.Remove(townToDelete);

            Console.WriteLine($"{addresses.Count()} addresses was deleted");
            db.SaveChanges();

        }
    }
}
