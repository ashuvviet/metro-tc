using System;
using System.IO;
using System.Reflection;
using MetroTransport.Application.Services;
using MetroTransport.Infra;
using MetroTransport.Infra.Services;
using Unity;

namespace MetroTransport.Application
{
  public class StartUp
  {
    public static void Initialize()
    {
      Host.Instance.Initialize(new UnityContainer());
      Host.Instance.AddSingleton<ILogService, LogService>();
      Host.Instance.AddScope<IFareCapService, FareCapService>();
      Host.Instance.AddScope<IFareRuleService, FareRuleService>();
      new Resolver().Resolve();

      Host.Instance.Get<ILogService>().RegisterObserver(new ConsoleLogger());
      RegisterFileObserver();
    }

    public static void Dispose()
    {
      Host.Instance.Get<ILogService>().Dispose();
      Host.Instance.Dispose();
    }

    [System.Diagnostics.Conditional("DEBUG")]
    private static void RegisterFileObserver()
    {
      var codeBase = Assembly.GetExecutingAssembly().CodeBase;
      var localPath = new Uri(codeBase).LocalPath;
      var path = Path.GetDirectoryName(localPath);
      if (string.IsNullOrWhiteSpace(path) == false)
      {
        Host.Instance.Get<ILogService>().RegisterObserver(new FileLogger(Path.Combine(path, "log.txt")));
      }
    }
  }
}
