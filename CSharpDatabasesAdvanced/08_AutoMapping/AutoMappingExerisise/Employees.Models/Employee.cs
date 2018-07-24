using System;
using System.Collections.Generic;

namespace Employees.Models
{
    public class Employee
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }
    }
}
