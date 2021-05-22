using System;
using System.Collections.Generic;

namespace MetroTransport.Domain
{
  public class WeeklyCap : BaseCap
  {
    public IList<BaseCap> DailyCaps { get; set; }
    
    public WeeklyCap(Journey journey) : base(journey.PunchTime)
    {
      SourceZone = journey.Source.Id;
      DestinationZone = journey.Destination.Id;
      DailyCaps = new List<BaseCap>() { new DailyCap(journey.PunchTime, this) };
      this.End = this.Start.AddDays(7 - (int)Start.DayOfWeek).AddHours(24 - Start.Hour - 1).AddMinutes(59 - Start.Minute);
    }

    public bool IsEqual(Journey journey) => this.End.Date >= journey.PunchTime.Date;

    public BaseCap this[DayOfWeek dayOfWeek]
    {
      get
      {
        foreach (var dailyCap in DailyCaps)
        {
          if (dailyCap.End.DayOfWeek == dayOfWeek)
          {
            return dailyCap;
          }
        }

        return null;
      }
    }

    public override void AddJourneyFare(double fare) => this.TotalCharge += fare;

    public override void ResetZone(int from, int to)
    {
      SourceZone = from;
      DestinationZone = to;
    }

    public BaseCap Add(DateTime start)
    {
      var today = new DailyCap(start, this);
      DailyCaps.Add(today);
      return today;
    }
  }
}