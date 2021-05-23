using MetroTransport.Application.Services;
using MetroTransport.Infra;
using MetroTransport.Infra.Services;
using MetroTransport.Rules;
using MetroTransport.UnitTests.Helper;
using NSubstitute;
using Xunit;

namespace MetroTransport.UnitTests
{
  public class FareCapServiceTests : BaseTest
  {
    [Theory]
    [InlineData(30, 30)]
    [InlineData(120, 100)]
    [InlineData(720, 100)]
    public void ApplyFareCapTest_Single(double currentfare, double expectedfare)
    {
      //Arrange
      var fareCapService = new FareCapService(Host.Instance.Get<ILogService>());
      fareCapService.AddCapFareRule(new DailyCapFareRule());
      fareCapService.AddCapFareRule(new WeeklyCapFareRule());
      var journey = GetNewJourney();

      // Act
      var fare = fareCapService.ApplyFareCap(journey, currentfare);


      // Assert
      Assert.Equal(expectedfare, fare);
    }

    [Theory]
    [InlineData(30, 30)]
    [InlineData(120, 20)]
    [InlineData(720, 20)]
    public void ApplyFareCapTest_Multiple(double currentfare, double expectedfare)
    {
      //Arrange
      var fareCapService = new FareCapService(Host.Instance.Get<ILogService>());
      fareCapService.AddCapFareRule(new DailyCapFareRule());
      fareCapService.AddCapFareRule(new WeeklyCapFareRule());
      var journey = GetNewJourney();
      fareCapService.ApplyFareCap(journey, currentfare);
      journey = GetNewJourney(1, 2);

      // Act
      var fare = fareCapService.ApplyFareCap(journey, currentfare);


      // Assert
      Assert.Equal(expectedfare, fare);
    }

    [Fact]
    public void GenerateReportTest()
    {
      //Arrange
      var fareCapService = new FareCapService(Host.Instance.Get<ILogService>());
      fareCapService.AddCapFareRule(new DailyCapFareRule());
      fareCapService.ApplyFareCap(GetNewJourney(), 30);

      // Act
      fareCapService.GenerateReport();


      // Assert
      Host.Instance.Get<ILogService>().Received().Log(Arg.Any<string>());
    }
  }
}
