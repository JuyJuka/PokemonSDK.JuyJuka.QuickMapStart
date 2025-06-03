namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Logging
{
  public class Logger : ILogger
  {
    public virtual void Write(string message)
    {
      Console.WriteLine(message);
    }
  }
}