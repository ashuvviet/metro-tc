using System.Collections.Generic;
using MetroTransport.Domain;
using MetroTransport.Infra.Services;

namespace MetroTransport.Application.Services
{
  public class FareCapService : IFareCapService
  {
    private readonly ILogService _logService;
    private readonly List<IFareCapRule> _fareCapRules;
    private readonly List<WeeklyCap> weeklyCaps;

    public FareCapService(ILogService logService)
    {
      _logService = logService;
      _fareCapRules = new List<IFareCapRule>();
      weeklyCaps = new List<WeeklyCap>();
    }

    public void AddCapFareRule(IFareCapRule rule)
    {
      _fareCapRules.Add(rule);
    }

    public double ApplyFareCap(Journey journey, double fare)
    {
      var dailyCap = GetDailyCap(journey);
      var revisedFare = fare;

      foreach (var fareCapRule in _fareCapRules)
      {
        if (fareCapRule.CanExecute(dailyCap.Parent.SourceZone, dailyCap.Parent.DestinationZone, dailyCap))
        {
          revisedFare = fareCapRule.Execute(dailyCap.Parent.SourceZone, dailyCap.Parent.DestinationZone, revisedFare, dailyCap);
          if (revisedFare == 0)
          {
            break;
          }
        }
      }

      dailyCap.AddJourneyFare(revisedFare);
      return revisedFare;
    }

    public void GenerateReport()
    {
      foreach (var weeklyCap in weeklyCaps)
      {
        _logService.Log($"Start of Week {weeklyCap.Start}.................................{weeklyCap.End}");
        _logService.Log("-------------");

        foreach (var weeklyCapDailyCap in weeklyCap.DailyCaps)
        {
          _logService.Log($"{weeklyCapDailyCap.Start}: {weeklyCapDailyCap.Start.DayOfWeek} Total Charges = {weeklyCapDailyCap.TotalCharge}");
        }

        _logService.Log($"End of Week {weeklyCap.Start}.................................{weeklyCap.End}");
        _logService.Log($"Total Week Cap Charges = {weeklyCap.TotalCharge}");
        _logService.Log("-------------");
      }
    }

    private BaseCap GetDailyCap(Journey journey)
    {
      var weeklyCap = GetWeeklyCap(journey);
      var currentDailyCap = weeklyCap[journey.PunchTime.DayOfWeek];

      if (currentDailyCap == null)
      {
        currentDailyCap = weeklyCap.Add(journey.PunchTime);
      }

      return currentDailyCap;
    }

    private WeeklyCap GetWeeklyCap(Journey journey)
    {
      WeeklyCap currentWeeklyCap = null;
      foreach (var weeklyCap in weeklyCaps)
      {
        if (weeklyCap.IsEqual(journey))
        {
          // set zone 1-2 if any single travel occurs.
          if (journey.Source.Id != journey.Destination.Id)
          {
            weeklyCap.ResetZone(journey.Source.Id, journey.Destination.Id);
          }

          currentWeeklyCap = weeklyCap;
          break;
        }
      }

      if (currentWeeklyCap == null)
      {
        currentWeeklyCap = new WeeklyCap(journey);
        weeklyCaps.Add(currentWeeklyCap);
      }

      return currentWeeklyCap;
    }

  }
}