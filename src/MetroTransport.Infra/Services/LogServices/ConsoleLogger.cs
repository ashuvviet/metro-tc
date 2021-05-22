using System;

namespace MetroTransport.Infra.Services
{
  public class ConsoleLogger : ILogger
  {
    public void Log(string logMessage)
    {
      Console.WriteLine(logMessage);
    }

    public void Dispose()
    {
    }
  }
}