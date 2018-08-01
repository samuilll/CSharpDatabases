
namespace TeamBuilder.App.Contracts
{
  public  interface ICommandDispatcher
    {
        string DispatchCommand(string[] commandParameters);
    }
}
