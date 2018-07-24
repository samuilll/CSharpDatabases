using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.ModelsDto
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public string ManagerLastName { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id} - {this.FirstName} {this.Lastname} -  ${this.Salary:f2}";
        }

        public string PersonalInfo()
        {
            return this.ToString() + $"{Environment.NewLine}Birthay: {this.Birthday}" + $"{Environment.NewLine}Address: {this.Address}";
        }

        public string OlderThanAgeInfo()
        {

            string result = this.ManagerLastName == null ? $"{this.FirstName} {this.Lastname} - ${this.Salary:f2}" +
                                                           $" - Manager: [no manager]" 
                                                         : $"{this.FirstName} {this.Lastname} - ${this.Salary:f2}" +
                                                           $" - Manager: {this.ManagerLastName}";

            return result;
        }
    }
}
