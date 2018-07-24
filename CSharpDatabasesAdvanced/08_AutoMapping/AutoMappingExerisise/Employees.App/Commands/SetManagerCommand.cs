using Employee.App.Commands.Contracts;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class SetManagerCommand : ICommand
    {
        private IEmployeeService employeeService;

        public SetManagerCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            var employeeId = int.Parse(commandArgs[0]);
            var managerId = int.Parse(commandArgs[1]);

            var result = this.employeeService.SetManager(employeeId, managerId);

            return result ;
        }
    }
}
