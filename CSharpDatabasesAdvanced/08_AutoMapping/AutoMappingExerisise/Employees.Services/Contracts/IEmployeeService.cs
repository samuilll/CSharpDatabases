using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services.Contracts
{
    public interface IEmployeeService
    {
        TModel AddEmployee<TModel>(string firstName,string lastName,decimal salary);

        TModel SetBirthDay<TModel>(int id, string dateTime);

        TModel SetAddress<TModel>(int id, string address);

        TModel EmployeeInfo<TModel>(int id);

        string SetManager(int employeeId, int managerId);

        TModel ManagerInfo<TModel>(int id);

        IEnumerable<TModel> OlderThanList<TModel>(int age);
    }
}
