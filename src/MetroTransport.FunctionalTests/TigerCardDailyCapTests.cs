using System;
using System.Collections.Generic;
using MetroTransport.Application;
using MetroTransport.Application.Services;
using MetroTransport.Domain;
using MetroTransport.Infra;
using Xunit;

namespace MetroTransport.FunctionalTests
{
  public class TigerCardDailyCapTests : IDisposable
  {
    [Fact]
    public void TigerCardDailyCapMultipleJournriesTest()
    {
      //Arrange
      var journeys = Setupprerequisite();


      // Act
      var fare = Host.Instance.Get<IFareRuleService>().CalculateFare(journeys);


      // Assert
      Assert.Equal(120, fare);
    }

    private IEnumerable<Journey> Setupprerequisite()
    {
      StartUp.Initialize();
      var datetime = new DateTime(2021, 5, 24, 10, 0, 0, DateTimeKind.Local);
      var journeys = new List<Journey>()
      {
        new Journey(new Zone(2, "central"), new Zone(1, "concentric"),  datetime),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddMinutes(45)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddHours(6).AddMinutes(15)),
        new Journey(new Zone(1, "central"), new Zone(1, "concentric"),  datetime.AddHours(8).AddMinutes(15)),
        new Journey(new Zone(1, "central"), new Zone(2, "concentric"),  datetime.AddHours(9))
      };

      return journeys;
    }

    public void Dispose()
    {
      StartUp.Dispose();
    }
  }
}
