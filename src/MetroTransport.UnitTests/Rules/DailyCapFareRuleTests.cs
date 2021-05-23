using MetroTransport.Domain;
using MetroTransport.Rules;
using MetroTransport.UnitTests.Helper;
using Xunit;

namespace MetroTransport.UnitTests.Rules
{
  public class DailyCapFareRuleTests : BaseTest
  {
    [Fact]
    public void CanExecuteTest_True()
    {
      //Arrange
      var rule = new DailyCapFareRule();

      // Act
      var result = rule.CanExecute(default, default, new DailyCap(default, null));

      // Assert
      Assert.True(result);
    }

    [Fact]
    public void CanExecuteTest_False()
    {
      //Arrange
      var rule = new DailyCapFareRule();

      // Act
      var result = rule.CanExecute(default, default, new WeeklyCap(GetNewJourney()));

      // Assert
      Assert.False(result);
    }

    [Theory]
    [InlineData(1, 1, 30, 30)]
    [InlineData(1, 1, 60, 40)]
    [InlineData(1, 1, 150, 0)]
    [InlineData(1, 2, 125, 0)]
    [InlineData(2, 1, 125, 0)]
    [InlineData(2, 2, 125, 0)]
    public void ExecuteTest(int from, int to, double fare, double expectedfare)
    {
      //Arrange
      var rule = new DailyCapFareRule();
      var journey = GetNewJourney();
      var dailyCap = new DailyCap(journey.PunchTime, new WeeklyCap(journey));
      dailyCap.AddJourneyFare(fare);

      // Act
      var result = rule.Execute(from, to,  fare, dailyCap);

      // Assert
      Assert.Equal(expectedfare, result);
    }
  }
}
