using System.Collections.Generic;

namespace MetroTransport.Domain
{
  public class Card
  {
    public IEnumerable<Journey> TotalJourneys { get; set; }
  }
}
