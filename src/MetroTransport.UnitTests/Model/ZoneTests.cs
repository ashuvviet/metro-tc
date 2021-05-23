using MetroTransport.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetroTransport.UnitTests.Model
{
  public class ZoneTests
  {
    [Fact]
    public void CheckZone_Name()
    {
      //Arrange
      // Act
      var zone = new Zone(1, "test");


      // Assert
      Assert.Equal(1, zone.Id);
      Assert.Equal("test", zone.Name);
    }
  }
}
