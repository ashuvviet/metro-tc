using System.Collections.Generic;
using MetroTransport.Domain;

namespace MetroTransport.Application.Services
{
  public interface IFareRuleService
  {
    public void AddFareRule(IFareBasedRule rule);

    double CalculateFare(Card card);

    double CalculateFare(IEnumerable<Journey> journeys);

    double CalculateFare(Journey journey);

    void GenerateReport();
  }
}
