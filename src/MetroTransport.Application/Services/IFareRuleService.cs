using System;
using System.Collections.Generic;
using System.Linq;
using MetroTransport.Domain;
using MetroTransport.Domain.Contracts;

namespace MetroTransport.Application.Services
{
  public interface IFareRuleService
  {
    public void AddFareRule(IFareBasedRule rule);

    double CalculateFare(IEnumerable<Journey> journeys);

    double CalculateFare(Journey journey);
  }

  public interface IFareCapService
  {
    void AddCapFareRule(IFareCapRule rule);

    double ReviseFareCap(Journey journey, double fare);
  }

  public class FareRuleService : IFareRuleService
  {
    private readonly IFareCapService _fareCapService;
    private readonly List<IFareBasedRule> _rules;

    public FareRuleService(IFareCapService fareCapService)
    {
      _fareCapService = fareCapService;
      _rules = new List<IFareBasedRule>();
    }
    public void AddFareRule(IFareBasedRule rule)
    {
      _rules.Add(rule);
    }

    public double CalculateFare(IEnumerable<Journey> journeys)
    {
      double totalFare = 0;
      foreach (var journey in journeys)
      {
        totalFare += CalculateFare(journey);
      }

      return totalFare;
    }

    public double CalculateFare(Journey journey)
    {
      var fareRule = this._rules.FirstOrDefault(s => s.CanExecute(journey.Source, journey.Destination, journey.PunchTime));
      if (fareRule == null)
      {
        // Log no cap rule defined.
        throw new InvalidOperationException("No cap rule defined");
      }

      var currentFare = fareRule.Execute(journey.Source, journey.Destination, journey.PunchTime);

      return _fareCapService.ReviseFareCap(journey, currentFare);
    }
  }


  public class FareCapService : IFareCapService
  {
    private readonly List<IFareCapRule> _rules;
    private readonly List<WeeklyCap> weeklyCaps;

    public FareCapService()
    {
      _rules = new List<IFareCapRule>();
      weeklyCaps = new List<WeeklyCap>();
    }
    public void AddCapFareRule(IFareCapRule rule)
    {
      _rules.Add(rule);
    }

    public double ReviseFareCap(Journey journey, double fare)
    {
      var revisedFare = fare;
      var capRule = this._rules.FirstOrDefault(s => s.CanExecute(journey.Source, journey.Destination));
      if (capRule == null)
      {
        // Log no cap rule defined.
        throw new InvalidOperationException("No cap rule defined");
      }

      var capMaxLimit = capRule.Execute(journey.Source, journey.Destination);
      var dailyCap = GetDailyCap(journey);

      // case: week cap reached.
      if (dailyCap.Parent.TotalCharge >= capMaxLimit.WeeklyCap)
      {
        return 0;
      }

      if (dailyCap.Parent.TotalCharge + fare >= capMaxLimit.WeeklyCap)
      {
        revisedFare = capMaxLimit.WeeklyCap - dailyCap.Parent.TotalCharge;
        dailyCap.Parent.AddJourneyFare(revisedFare);
        return revisedFare;
      }

      // case; daily cap reached.
      if (dailyCap.TotalCharge >= capMaxLimit.DailyCap)
      {
        revisedFare = 0;
      }

      if (dailyCap.TotalCharge + fare >= capMaxLimit.DailyCap)
      {
        revisedFare = capMaxLimit.DailyCap - dailyCap.TotalCharge;
      }

      dailyCap.AddJourneyFare(revisedFare);
      return revisedFare;
    }

    private BaseCap GetDailyCap(Journey journey)
    {
      var weeklyCap = GetWeeklyCap(journey);
      BaseCap currentDailyCap = null;
      foreach (var dailyCap in weeklyCap.DailyCaps)
      {
        if (dailyCap.End.DayOfWeek == journey.PunchTime.DayOfWeek)
        {
          currentDailyCap = dailyCap;
          break;
        }
      }

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
        if (weeklyCap.End.Date <= journey.PunchTime.Date)
        {
          currentWeeklyCap = weeklyCap;
          break;
        }
      }

      if (currentWeeklyCap == null)
      {
        currentWeeklyCap = new WeeklyCap(journey.PunchTime);
      }

      return currentWeeklyCap;
    }

  }
}
