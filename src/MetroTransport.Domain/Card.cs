using System;
using System.Collections.Generic;
using System.ComponentModel;
using MetroTransport.Domain.Contracts;

namespace MetroTransport.Domain
{
  public class Card
  {
    public IEnumerable<Journey> TotalJourneys { get; set; }
  }


  public class Zone
  {
    public int Id { get; set;  }

    public string Name { get; set; }
  }

  public class Journey
  {
    public Zone Source { get; }
    public Zone Destination { get; }

    public DateTime PunchTime { get; }

    public Journey(Zone source, Zone destination, DateTime punchTime)
    {
      Destination = destination;
      Source = source;
      PunchTime = punchTime;
    }
  }

  public abstract class BaseCap
  {
    protected BaseCap(DateTime start)
    {
      Start = start;
    }

    public DateTime Start { get; }

    public DateTime End { get; protected set; }

    public BaseCap Parent { get; protected set; }

    public double TotalCharge { get; protected set; }

    public abstract void AddJourneyFare(double fare);
  }

  public class WeeklyCap : BaseCap
  {
    public IList<BaseCap> DailyCaps { get; set; }

    public WeeklyCap(DateTime start) : base(start)
    {
      DailyCaps = new List<BaseCap>() { new DailyCap(start, this)};
      this.End = this.Start.AddDays(7 - (int)Start.DayOfWeek);
    }

    public override void AddJourneyFare(double fare)
    {
      this.TotalCharge += fare;
    }

    public BaseCap Add(DateTime start)
    {
      var today = new DailyCap(start, this);
      DailyCaps.Add(today);
      return today;
    }
  }

  public class DailyCap : BaseCap
  {
    public DailyCap(DateTime start, BaseCap parent) : base(start)
    {
      this.Parent = parent;
      this.End = start;
    }

    public override void AddJourneyFare(double fare)
    {
      this.TotalCharge += fare;
      this.Parent.AddJourneyFare(fare);
    }
  }
}
