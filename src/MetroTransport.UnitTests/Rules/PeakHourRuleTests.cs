using System;
using MetroTransport.Rules;
using MetroTransport.UnitTests.Helper;
using Xunit;

namespace MetroTransport.UnitTests.Rules
{
  public class PeakHourRuleTests : BaseTest
  {
    [Theory]
    [InlineData(1, 1, 24, 8, true)]
    [InlineData(1, 1, 24, 18, false)]
    [InlineData(1, 1, 30, 19, false)]
    [InlineData(1, 2, 24, 18, true)]
    [InlineData(1, 1, 24, 11, false)]
    [InlineData(1, 1, 29, 10, true)]
    [InlineData(1, 2, 30, 17, true)]
    [InlineData(1, 2, 30, 21, true)]
    [InlineData(1, 2, 30, 15, false)]
    public void CanExecuteTest(int sourceZone, int destinationZone, int day, int hour, bool expectedOutput)
    {
      //Arrange
      var rule = new PeakHourRule();
      var journey = GetNewJourney(sourceZone, destinationZone);
      var datetime = new DateTime(2021, 5, day, hour, 0, 0, DateTimeKind.Local);

      // Act
      var result = rule.CanExecute(journey.Source, journey.Destination, datetime);

      // Assert
      Assert.Equal(expectedOutput, result);
    }

    [Theory]
    [InlineData(1, 1, 30)]
    [InlineData(1, 2, 35)]
    [InlineData(2, 1, 35)]
    [InlineData(2, 2, 25)]
    [InlineData(3, 2, 0)]
    public void ExecuteTest(int sourceZone, int destinationZone, int expectedOutput)
    {
      //Arrange
      var rule = new PeakHourRule();
      var journey = GetNewJourney(sourceZone, destinationZone);
      var datetime = new DateTime(2021, 5, 24, 10, 0, 0, DateTimeKind.Local);

      // Act
      var result = rule.Execute(journey.Source, journey.Destination, datetime);

      // Assert
      Assert.Equal(expectedOutput, result);
    }
  }
}
