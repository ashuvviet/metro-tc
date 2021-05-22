using System;
using System.ComponentModel.Composition;
using MetroTransport.Domain;
using MetroTransport.Infra.Extensions;


namespace MetroTransport.Rules
{
  [Export(typeof(IFareBasedRule))]
  public class OffPeakHourRule : IFareBasedRule
  {
    public bool CanExecute(Zone source, Zone destination, DateTime time)
    {
      if (source.Id == 1 && destination.Id == 1 && time.IsOffPeekHour())
      {
        return true;
      }

      return !time.IsPeekHour();
    }

    public int Execute(Zone source, Zone destination, DateTime time)
    {
      var cost = 0;
      switch (source.Id, destination.Id)
      {
        case (1, 1):
          cost = 25;
          break;
        case (1, 2):
        case (2, 1):
          cost = 30;
          break;
        case (2, 2):
          cost = 20;
          break;
      }

      return cost;
    }
  }
}
