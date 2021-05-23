using MetroTransport.Domain;
using MetroTransport.Rules;
using MetroTransport.UnitTests.Helper;
using Xunit;

namespace MetroTransport.UnitTests.Rules
{
  public class WeeklyCapFareRuleTests : BaseTest
  {
    [Fact]
    public void CanExecuteTest_False()
    {
      //Arrange
      var rule = new WeeklyCapFareRule();

      // Act
      var result = rule.CanExecute(default, default, new DailyCap(default, null));

      // Assert
      Assert.False(result);
    }

    [Fact]
    public void CanExecuteTest_True()
    {
      //Arrange
      var rule = new WeeklyCapFareRule();

      // Act
      var result = rule.CanExecute(default, default, new DailyCap(default, new WeeklyCap(GetNewJourney())));

      // Assert
      Assert.True(result);
    }

    [Theory]
    [InlineData(1, 1, 490, 10)]
    [InlineData(1, 1, 60, 60)]
    [InlineData(1, 1, 540, 0)]
    [InlineData(1, 2, 610, 0)]
    [InlineData(2, 1, 700, 0)]
    [InlineData(2, 1, 550, 50)]
    [InlineData(2, 2, 400, 0)]
    public void ExecuteTest(int from, int to, double fare, double expectedfare)
    {
      //Arrange
      var rule = new WeeklyCapFareRule();
      var cap = new DailyCap(default, new WeeklyCap(GetNewJourney()));
      cap.Parent.AddJourneyFare(fare);

      // Act
      var result = rule.Execute(from, to,  fare, cap);

      // Assert
      Assert.Equal(expectedfare, result);
    }
  }
}
