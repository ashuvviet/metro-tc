using System;

namespace MetroTransport.Domain
{
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
}