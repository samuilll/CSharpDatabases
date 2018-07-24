

namespace Employees.Contracts
{
  public  interface ICommandDispatcher
    {
        string DispatchCommand(string[] commandParameters);
    }
}
