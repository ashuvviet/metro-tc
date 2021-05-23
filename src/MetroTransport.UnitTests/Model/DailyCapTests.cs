using MetroTransport.Domain;
using MetroTransport.UnitTests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetroTransport.UnitTests.Model
{
  public class DailyCapTests : BaseTest
  {
    [Fact]
    public void DailyCap_Tests()
    {
      //Arrange
      // Act
      var dailyCap = new DailyCap(DateTime.Now, null);


      // Assert
      Assert.Null(dailyCap.Parent);
    }

    [Fact]
    public void DailyCap_AddFare_Tests()
    {
      //Arrange
      var dailyCap = new DailyCap(DateTime.Now, new WeeklyCap(GetNewJourney()));

      // Act
      dailyCap.AddJourneyFare(10);

      // Assert
      Assert.NotNull(dailyCap.Parent);
      Assert.Equal(10, dailyCap.TotalCharge);
      Assert.Equal(10, dailyCap.Parent.TotalCharge);
    }
  }
}
