using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class EmployeeInfoCommand : ICommand
    {

        private IEmployeeService employeeService;

        public EmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            int id = int.Parse(commandArgs[0]);

            var dto = this.employeeService.EmployeeInfo<EmployeeDto>(id);

            return dto.ToString();
        }
    }
}
