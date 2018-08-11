using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services
{
    using Contracts;
    using Employees.Data;
    using Employees.Models;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        private const string successfulSetManagerMessage = "{0} {1} was successfully set as a manager to {2} {3}";

        private IMapper mapper;

        public EmployeeService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel AddEmployee<TModel>(string firstName, string lastName, decimal salary)
        {
            using (var db = new EmployeeDbContext() )
            {
                var employee = new Employee()
                {
                    FirstName = firstName,
                    Lastname = lastName,
                    Salary = salary
                };

                db.Employees.Add(employee);

                db.SaveChanges();

                var dto = mapper.Map<TModel>(employee);

                return dto;
            }

            throw new Exception();
        }

        public TModel EmployeeInfo<TModel>(int id)
        {
            using (var db = new EmployeeDbContext())
            {
                var employee = db.Employees.Find(id);

                var dto = mapper.Map<TModel>(employee);

                return dto;
            }
        }

        public TModel ManagerInfo<TModel>(int id)
        {
            using (var db = new EmployeeDbContext())
            {
                var manager = db.Employees.Find(id);

                var dto = mapper.Map<TModel>(manager);

                return dto;
            }
        }

        public IEnumerable<TModel> OlderThanList<TModel>(int age)
        {
            using (var db = new EmployeeDbContext())
            {

                var employeesDtos = db.Employees
                    .Where(e => e.Birthday != null)
                    .Where(e => (DateTime.Now - e.Birthday).Value.TotalDays > age * 365.2422)
                    .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                    .ToList();

                return employeesDtos;

            }
        }

        public TModel SetAddress<TModel>(int id,string address)
        {
            using (var db = new EmployeeDbContext())
            {
                var employee = db.Employees.Find(id);

                employee.Address = address;

                db.SaveChanges();

                var dto = mapper.Map<TModel>(employee);

                return dto;
            }
        }

        public TModel SetBirthDay<TModel>(int id, string dateTime)
        {
            using ( var db = new EmployeeDbContext())
            {
                var employee = db.Employees.Find(id);

                var birthay = DateTime.Parse(dateTime);

                employee.Birthday = birthay;

                db.SaveChanges();

                var dto = mapper.Map<TModel>(employee);

                return dto;
            }
        }

        public string SetManager(int employeeId, int managerId)
        {
            using (var db = new EmployeeDbContext())
            {
                var manager = db.Employees.Find(managerId);

                var employee = db.Employees.Find(employeeId);

                employee.ManagerId = managerId;

                db.SaveChanges();

                return string.Format(successfulSetManagerMessage,manager.FirstName,manager.Lastname,employee.FirstName,employee.Lastname);        
            }
        }
    }
}
