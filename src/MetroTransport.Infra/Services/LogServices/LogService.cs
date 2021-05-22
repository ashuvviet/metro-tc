using System;
using System.Collections.Generic;
using System.Globalization;

namespace MetroTransport.Infra.Services
{
  public interface ILogger : IDisposable
  {
    void Log(string message);
  }

  public class LogService : ILogService
  {
    private static readonly IDictionary<Type, ILogger> observers = new Dictionary<Type, ILogger>();

    public void RegisterObserver(ILogger instance) => observers[instance.GetType()] = instance;

    public void Log(string message)
    {
      foreach (ILogger observer in observers.Values)
      {
        observer.Log(message);
      }
    }

    public void Dispose()
    {
      foreach (ILogger observer in observers.Values)
      {
        observer.Dispose();
      }
    }
  }
}
