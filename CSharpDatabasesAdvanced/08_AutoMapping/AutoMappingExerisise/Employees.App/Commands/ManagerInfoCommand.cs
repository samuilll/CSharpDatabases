using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        public const string noManagermessage = "{0} {1} is not a manager!";

        private IEmployeeService employeeService;

        public ManagerInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            int id = int.Parse(commandArgs[0]);

            var dto = this.employeeService.ManagerInfo<ManagerDto>(id);

            if (dto.EmployeeCount==0)
            {
                return string.Format(noManagermessage,dto.FirstName,dto.LastName);
            }

            return dto.ToString();
        }
    }
}
