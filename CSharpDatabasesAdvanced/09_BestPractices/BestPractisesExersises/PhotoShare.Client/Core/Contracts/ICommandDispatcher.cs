

namespace PhotoShare.Client.Core.Contracts
{
  public  interface ICommandDispatcher
    {
        string DispatchCommand(string[] commandParameters);
    }
}
