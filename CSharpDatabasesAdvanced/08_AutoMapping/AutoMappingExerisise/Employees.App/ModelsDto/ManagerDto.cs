using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.ModelsDto
{
   public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> EmployeesDto { get; set; }

        public int EmployeeCount => this.EmployeesDto.Count;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.EmployeeCount}");

            foreach (var employee in this.EmployeesDto)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.Lastname} - ${employee.Salary:f2}");
            }
            return sb.ToString();
        }


    }
}
