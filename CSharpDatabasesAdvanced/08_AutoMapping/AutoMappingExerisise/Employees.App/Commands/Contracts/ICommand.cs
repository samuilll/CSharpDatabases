namespace Employee.App.Commands.Contracts
{
  public  interface ICommand
    {
        string Execute(params string[] commandArgs);
    }
}
