using System;
using System.Collections.Generic;
using MetroTransport.Application.Services;
using MetroTransport.Domain;
using MetroTransport.Infra;
using MetroTransport.Infra.Services;
using MetroTransport.Rules;
using MetroTransport.UnitTests.Helper;
using NSubstitute;
using Xunit;

namespace MetroTransport.UnitTests.Application
{
  public class FareRuleServiceTests : BaseTest
  {
    [Theory]
    [InlineData(30)]
    [InlineData(120)]
    [InlineData(720)]
    public void CalculateFare_Journey(double expectedfare)
    {
      //Arrange
      var fareCapMock = Substitute.For<IFareCapService>();
      fareCapMock.ApplyFareCap(Arg.Any<Journey>(), Arg.Any<double>()).Returns(expectedfare);
      var fareCapService = new FareRuleService(fareCapMock, Host.Instance.Get<ILogService>());
      fareCapService.AddFareRule(new PeakHourRule());
      fareCapService.AddFareRule(new OffPeakHourRule());
      var journey = GetNewJourney();


      // Act
      var fare = fareCapService.CalculateFare(journey);


      // Assert
      Assert.Equal(expectedfare, fare);
    }

    [Theory]
    [InlineData(30)]
    [InlineData(120)]
    public void CalculateFare_journeys(double expectedfare)
    {
      //Arrange
      var fareCapMock = Substitute.For<IFareCapService>();
      fareCapMock.ApplyFareCap(Arg.Any<Journey>(), Arg.Any<double>()).Returns(expectedfare);
      var fareCapService = new FareRuleService(fareCapMock, Host.Instance.Get<ILogService>());
      fareCapService.AddFareRule(new PeakHourRule());
      fareCapService.AddFareRule(new OffPeakHourRule());
      var journey = GetNewJourney();


      // Act
      var fare = fareCapService.CalculateFare(new List<Journey>() { journey });


      // Assert
      Assert.Equal(expectedfare, fare);
    }

    [Theory]
    [InlineData(30)]
    [InlineData(120)]
    public void CalculateFare_Card(double expectedfare)
    {
      //Arrange
      var fareCapMock = Substitute.For<IFareCapService>();
      fareCapMock.ApplyFareCap(Arg.Any<Journey>(), Arg.Any<double>()).Returns(expectedfare);
      var fareCapService = new FareRuleService(fareCapMock, Host.Instance.Get<ILogService>());
      fareCapService.AddFareRule(new PeakHourRule());
      fareCapService.AddFareRule(new OffPeakHourRule());
      var journey = GetNewJourney();


      // Act
      var fare = fareCapService.CalculateFare(new Card() { TotalJourneys = new List<Journey>() { journey }});


      // Assert
      Assert.Equal(expectedfare, fare);
    }

    [Fact]
    public void CalculateFare_NoRulesDefined_Exception()
    {
      //Arrange
      var fareCapMock = Substitute.For<IFareCapService>();
      var fareCapService = new FareRuleService(fareCapMock, Host.Instance.Get<ILogService>());
      var journey = GetNewJourney();


      // Act
      void action() => fareCapService.CalculateFare(journey);


      // Assert
      Assert.Throws<InvalidOperationException>(action);
    }
  }
}
