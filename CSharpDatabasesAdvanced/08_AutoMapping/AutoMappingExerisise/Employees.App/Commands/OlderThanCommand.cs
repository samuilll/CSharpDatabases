using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;
using System.Text;

namespace Employees.App.Commands
{
   public class OlderThanCommand:ICommand
    {
        private readonly IEmployeeService employeeService;

        public OlderThanCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            var age = int.Parse(commandArgs[0]);

            var dtos = this.employeeService.OlderThanList<EmployeeDto>(age);

            var sb = new StringBuilder();

            foreach (var employeeDto in dtos)
            {
                sb.AppendLine(employeeDto.OlderThanAgeInfo());
            }
            return sb.ToString();

        }
    }
}
