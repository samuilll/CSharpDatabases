using Employee.App.Commands.Contracts;
using Employees.App.ModelsDto;
using Employees.Services.Contracts;

namespace Employees.App.Commands
{
    public class SetBirthayCommand : ICommand
    {
        private const string successfulMessage = "Birthay of {0} was successfully set to {1}";
        private IEmployeeService employeeService;

        public SetBirthayCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] commandArgs)
        {
            int id = int.Parse(commandArgs[0]);

            string dateTimeString = commandArgs[1];

          var dto =   this.employeeService.SetBirthDay<EmployeeDto>(id,dateTimeString);

            return string.Format(successfulMessage, dto.Lastname, dto.Birthday.Value.ToString("dd-MM-yyyy"));
        }
    }
}
