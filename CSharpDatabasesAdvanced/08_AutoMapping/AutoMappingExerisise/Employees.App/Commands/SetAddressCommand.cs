using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class SetAddressCommand : ICommand
    {
        private const string successfulMessage = "Address of {0} was successfully set to {1}";

        private IEmployeeService employeeService;

        public SetAddressCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            int id = int.Parse(commandArgs[0]);

            string address = commandArgs[1];

            var dto = this.employeeService.SetAddress<EmployeeDto>(id, address);

            return string.Format(successfulMessage, dto.Lastname, dto.Address);
        }
    }
}
