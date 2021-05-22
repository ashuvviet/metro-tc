using System;

namespace MetroTransport.Domain
{
  public interface IFareBasedRule
  {
    bool CanExecute(Zone source, Zone destination, DateTime time);

    int Execute(Zone source, Zone destination, DateTime time);
  }
}
