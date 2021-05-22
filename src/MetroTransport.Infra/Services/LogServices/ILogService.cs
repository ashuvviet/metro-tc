using System;

namespace MetroTransport.Infra.Services
{
  public interface ILogService : IDisposable
  {
    void Log(string message);

    void RegisterObserver(ILogger instance);
  }
}