using System;

namespace MetroTransport.Domain
{
  public class DailyCap : BaseCap
  {
    public DailyCap(DateTime start, BaseCap parent) : base(start)
    {
      this.Parent = parent;
      this.End = start.AddHours(24 - start.Hour - 1).AddMinutes(59 - start.Minute); 
    }

    public override void AddJourneyFare(double fare)
    {
      this.TotalCharge += fare;
      this.Parent.AddJourneyFare(fare);
    }
  }
}