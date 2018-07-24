using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private const string successfullMessage = "Employee {0} {1} with Id {2} was added successfully!";

        private IEmployeeService employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            string firstName = commandArgs[0];
            string lastName = commandArgs[1];
            decimal salary = decimal.Parse(commandArgs[2]);

           var employeeDto =  this.employeeService.AddEmployee<EmployeeDto>(firstName,lastName,salary);

            return string.Format(successfullMessage,employeeDto.FirstName,employeeDto.Lastname,employeeDto.Id);
        }
    }
}
