using System;
using System.Collections.Generic;
using System.Linq;
using MetroTransport.Domain;
using MetroTransport.Infra.Services;

namespace MetroTransport.Application.Services
{
  public class FareRuleService : IFareRuleService
  {
    private readonly IFareCapService _fareCapService;
    private readonly ILogService _logService;
    private readonly List<IFareBasedRule> _rules;

    public FareRuleService(IFareCapService fareCapService, ILogService logService)
    {
      _fareCapService = fareCapService;
      _logService = logService;
      _rules = new List<IFareBasedRule>();
    }
    public void AddFareRule(IFareBasedRule rule)
    {
      _rules.Add(rule);
    }

    public double CalculateFare(Card card)
    {
      return CalculateFare(card.TotalJourneys);
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
        _logService.Log("No cap rule defined");
        throw new InvalidOperationException("No cap rule defined");
      }

      var currentFare = fareRule.Execute(journey.Source, journey.Destination, journey.PunchTime);

      return _fareCapService.ApplyFareCap(journey, currentFare);
    }

    public void GenerateReport() => _fareCapService.GenerateReport();
  }
}