using System;

namespace MetroTransport.Domain
{
  public abstract class BaseCap
  {
    protected BaseCap(DateTime start)
    {
      Start = start;
    }

    public DateTime Start { get; }

    public DateTime End { get; protected set; }

    public BaseCap Parent { get; protected set; }

    public int SourceZone { get; protected set; }

    public int DestinationZone { get; protected set; }

    public double TotalCharge { get; protected set; }

    public abstract void AddJourneyFare(double fare);

    public virtual void ResetZone(int from, int to)
    {
    }
  }
}