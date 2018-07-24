using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;
using System;

namespace Employees.App.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {

        private IEmployeeService employeeService;

        public EmployeePersonalInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            int id = int.Parse(commandArgs[0]);

            var dto = this.employeeService.EmployeeInfo<EmployeeDto>(id);

            return dto.PersonalInfo();
        }
    }
}
