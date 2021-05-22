using System;
using System.IO;

namespace MetroTransport.Infra.Services
{
  public class FileLogger : ILogger
  {
    private StreamWriter logFile;

    public FileLogger(string fileName)
    {
      Initialize(fileName);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    private void Initialize(string fileName)
    {
      if (!File.Exists(fileName))
      {
        using (File.Create(fileName)) { }
      }

      try
      {
        logFile = new StreamWriter(fileName, true) { AutoFlush = true };
      }
      catch (Exception)
      {
        logFile = null;
      }
    }

    public void Log(string logMessage)
    {
      logFile?.WriteLine(logMessage);
    }

    public void Dispose()
    {
      logFile?.Close();
      logFile = null;
    }
  }
}