using MetroTransport.Domain;
using MetroTransport.Infra.Attributes;


namespace MetroTransport.Rules
{
  [CapRule(2, typeof(IFareCapRule))]
  public class DailyCapFareRule : IFareCapRule
  {
    public bool CanExecute(int source, int destination, BaseCap cap) => cap is DailyCap;

    public double Execute(int from, int to, double currentfare, BaseCap cap)
    {
      var dailyCap = 0;
      var revisedFare = currentfare;
      switch (from, to)
      {
        case (1, 1):
          dailyCap = 100;
          break;
        case (1, 2):
        case (2, 1):
          dailyCap = 120;
          break;
        case (2, 2):
          dailyCap = 80;
          break;
      }

      // case; daily cap reached.
      if (cap.TotalCharge >= dailyCap)
      {
        revisedFare = 0;
      }

      if (cap.TotalCharge + revisedFare >= dailyCap)
      {
        revisedFare = dailyCap - cap.TotalCharge;
      }

      return revisedFare;
    }
  }
}
