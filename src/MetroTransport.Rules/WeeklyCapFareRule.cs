using MetroTransport.Domain;
using MetroTransport.Infra.Attributes;

namespace MetroTransport.Rules
{
  [CapRule(1, typeof(IFareCapRule))]
  public class WeeklyCapFareRule : IFareCapRule
  {
    public bool CanExecute(int source, int destination, BaseCap cap) => cap.Parent is WeeklyCap;

    public double Execute(int from, int to, double currentFare, BaseCap cap)
    {
      var weeklyCap = 0;
      var revisedFare = currentFare;
      switch (from, to)
      {
        case (1, 1):
          weeklyCap = 500;
          break;
        case (1, 2):
        case (2, 1):
          weeklyCap = 600;
          break;
        case (2, 2):
          weeklyCap = 400;
          break;
      }

      // case; daily cap reached.
      if (cap.Parent.TotalCharge >= weeklyCap)
      {
        revisedFare = 0;
      }

      if (cap.Parent.TotalCharge + revisedFare >= weeklyCap)
      {
        revisedFare = weeklyCap - cap.Parent.TotalCharge;
      }

      return revisedFare;
    }
  }
}