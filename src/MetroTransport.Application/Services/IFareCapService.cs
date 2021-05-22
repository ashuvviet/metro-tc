using MetroTransport.Domain;

namespace MetroTransport.Application.Services
{
  public interface IFareCapService
  {
    void AddCapFareRule(IFareCapRule rule);

    double ApplyFareCap(Journey journey, double fare);

    void GenerateReport();
  }
}