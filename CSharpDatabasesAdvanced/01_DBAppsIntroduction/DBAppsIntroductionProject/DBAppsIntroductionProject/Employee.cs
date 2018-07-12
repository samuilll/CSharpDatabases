using System;
using System.Collections.Generic;
using System.Text;

namespace DBAppsIntroductionProject
{
  public  class Employee:Person
    {
        public Employee(string firstName,string lastName,decimal salary,string jobTitle)
            :base(firstName,lastName)
        {
            this.Salary = salary;
            this.JobTitle = jobTitle;

        }

        public decimal Salary { get; }

        public string JobTitle { get; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} working as {this.JobTitle} earns ${this.Salary}";
        }
    }
}
